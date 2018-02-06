using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NSubstitute;
using Waes.Core.Interfaces;
using Waes.Core.Models;
using Waes.Infrastructure.Repositories;
using Waes.WaesDiff;

namespace Waes.Test
{
    [TestClass]
    public class UnitTest
    {
        private readonly string _base64Data1 = @"Qk2uAAAAAAAAADYAAAAoAAAABwAAAAUAAAABABgAAAAAAHgAAAAAAAAAAAAAAAAAAAAAAAAATLEiTLEiTLEiTLEiTLEiOXVEMlxTAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTEEEEEEEEEEEEEEEEEEEEAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAA";
        private readonly string _base64Data2 = @"Qk2uAAAABBBBBDYAAAAoAAAABwAAAAUAAAABABgAAAAAAHgAAAAAAAAAAAAAAAAAAAAAAAAATLEiTLEiTLEiTLEiTLEiOXVEMlxTAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAA";
        private readonly string _base64Data3 = @"Qk2uAAAABBBBBDYAAAAoAAAABwAAAAUAAAABABgAAAAAAHgAAAAAAAAAAAAAAAAAAAAAAAAA";
        private readonly string _key = "1";
        private readonly byte[] _byteArray1 = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly byte[] _byteArray2 = new byte[] { 1, 2, 2, 2, 2, 2, 2, 8, 9 };

        private IDiffResultRepository _repository;
        private IDiffService _diffService;
        private DiffResult _diffResult;

        [TestInitialize]
        public void StartUp()
        {
            _repository = Substitute.For<IDiffResultRepository>();
            _diffService = new DiffService(_repository);
        }
        
        [TestMethod]
        public void GivenTwoEqualsBase64EncodedBinaryData_WhenComparingData_Then_EqualFilesFlagShouldBeTrue_DifferentSizeFlagShouldBeFalse()
        {
            GivenTwoEqualsBase64EncodedBinaryData();
            WhenComparingData();
            EqualFilesFlagShouldBeTrue();
            DifferentSizeFlagShouldBeFalse();
            _diffResult.Diffs.Count.Should().Be(0);
            _diffResult.Messages.ToList()[0].Should().Be("Both files are equal");
        }

        [TestMethod]
        public void GivenTwoDifferentBase64EncodedBinaryData_WhenComparingData_Then_EqualFilesFlagShouldBeFalse_DifferentSizeFlagShouldBeFalse()
        {
            GivenTwoDifferentBase64EncodedBinaryData();
            WhenComparingData();
            EqualFilesFlagShouldBeFalse();
            DifferentSizeFlagShouldBeFalse();
            _diffResult.Diffs.Count.Should().Be(2);
            _diffResult.Messages.ToList().Count.Should().Be(2);
        }

        [TestMethod]
        public void GivenTwoDifferentSizeBase64_WhenComparingData_Then_DifferentSizeFlagShouldBeTrue()
        {
            GivenTwoDifferentSizeBase64();
            WhenComparingData();
            DifferentSizeFlagShouldBeTrue();
        }

        [TestMethod]
        public void GivenTwoEqualByteArrays_WhenComparingByteArrays_Then_EqualFilesFlagShouldBeTrue()
        {
            var byteArrays = GivenTwoEqualByteArrays();
            WhenComparingByteArrays(byteArrays[0], byteArrays[1]);
            EqualFilesFlagShouldBeTrue();
        }

        [TestMethod]
        public void GivenTwoDifferentByteArrays_WhenComparingByteArrays_Then_EqualFilesFlagShouldBeFalse()
        {
            var byteArrays = GivenTwoDifferentByteArrays();
            WhenComparingByteArrays(byteArrays[0], byteArrays[1]);
            EqualFilesFlagShouldBeFalse();
            _diffResult.Messages.ToList()[0].Should().Be("Difference found at position 2 with length of 5");
            _diffResult.Diffs.Count.Should().Be(1);
        }

        private List<byte[]> GivenTwoDifferentByteArrays()
        {
            return new List<byte[]>() { _byteArray1, _byteArray2 };
        }

        private List<byte[]> GivenTwoEqualByteArrays()
        {
            return new List<byte[]>() {_byteArray1, _byteArray1};
        }

        private void WhenComparingByteArrays(byte[] byteArray1, byte[] byteArray2)
        {
            _diffResult = _diffService.CompareBytes(byteArray1, byteArray2);
        }

        private void GivenTwoDifferentSizeBase64()
        {
            _repository.GetLeftBase64(_key).Returns(_base64Data1);
            _repository.GetRightBase64(_key).Returns(_base64Data3);

            _diffService = new DiffService(_repository);
        }

        private void DifferentSizeFlagShouldBeTrue()
        {
            _diffResult.DifferentSize.Should().BeTrue();
        }

        private void DifferentSizeFlagShouldBeFalse()
        {
            _diffResult.DifferentSize.Should().BeFalse();
        }

        private void WhenComparingData()
        {
            _diffResult = _diffService.CompareLeftAndRightFiles(_key);
        }

        private void EqualFilesFlagShouldBeTrue()
        {
            _diffResult.EqualFiles.Should().BeTrue();
        }

        private void EqualFilesFlagShouldBeFalse()
        {
            _diffResult.EqualFiles.Should().BeFalse();
        }

        private void GivenTwoEqualsBase64EncodedBinaryData()
        {
            _repository.GetLeftBase64(_key).Returns(_base64Data1);
            _repository.GetRightBase64(_key).Returns(_base64Data1);

            _diffService = new DiffService(_repository);
        }
        private void GivenTwoDifferentBase64EncodedBinaryData()
        {
            _repository.GetLeftBase64(_key).Returns(_base64Data1);
            _repository.GetRightBase64(_key).Returns(_base64Data2);

            _diffService = new DiffService(_repository);
        }
    }
}
