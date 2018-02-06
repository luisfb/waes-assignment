using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Waes.Infrastructure.Repositories;
using Waes.WaesDiff;

namespace Waes.Test
{
    [TestClass]
    public class IntegrationTest
    {
        private readonly string _base64Data1 = @"Qk2uAAAAAAAAADYAAAAoAAAABwAAAAUAAAABABgAAAAAAHgAAAAAAAAAAAAAAAAAAAAAAAAATLEiTLEiTLEiTLEiTLEiOXVEMlxTAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTEEEEEEEEEEEEEEEEEEEEAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAA";
        private readonly string _base64Data2 = @"Qk2uAAAABBBBBDYAAAAoAAAABwAAAAUAAAABABgAAAAAAHgAAAAAAAAAAAAAAAAAAAAAAAAATLEiTLEiTLEiTLEiTLEiOXVEMlxTAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAATLEiTLEiTLEiTLEiTLEiTLEiTLEiAAAA";
        private readonly string _base64Data3 = @"Qk2uAAAABBBBBDYAAAAoAAAABwAAAAUAAAABABgAAAAAAHgAAAAAAAAAAAAAAAAAAAAAAAAA";
        private readonly string _key = "1";
        private readonly byte[] _byteArray1 = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly byte[] _byteArray2 = new byte[] { 1, 2, 2, 2, 2, 2, 2, 8, 9 };

        private DiffService _diffService;

        [TestInitialize]
        public void StartUp()
        {
            _diffService = new DiffService(new DiffResultRepository(new InMemoryRepository()));
        }

        [TestMethod]
        public void GivenTwoEqualBase64_WhenComparingData_Then_EqualFilesFlagShouldBeTrue_DifferentSizeFlagShouldBeFalse()
        {
            _diffService.AddLeftFileToCompare(_key, _base64Data1);
            _diffService.AddRightFileToCompare(_key, _base64Data1);

            var result = _diffService.CompareLeftAndRightFiles(_key);

            result.EqualFiles.Should().BeTrue();
            result.DifferentSize.Should().BeFalse();
        }

        [TestMethod]
        public void GivenTwoDifferentBase64_WhenComparingData_Then_EqualFilesFlagShouldBeFalse_DifferentSizeFlagShouldBeFalse()
        {
            _diffService.AddLeftFileToCompare(_key, _base64Data1);
            _diffService.AddRightFileToCompare(_key, _base64Data2);

            var result = _diffService.CompareLeftAndRightFiles(_key);

            result.EqualFiles.Should().BeFalse();
            result.DifferentSize.Should().BeFalse();
        }

        [TestMethod]
        public void GivenTwoDifferentSizeBase64_WhenComparingData_Then_DifferentSizeFlagShouldBeTrue()
        {
            _diffService.AddLeftFileToCompare(_key, _base64Data1);
            _diffService.AddRightFileToCompare(_key, _base64Data3);

            var result = _diffService.CompareLeftAndRightFiles(_key);

            result.DifferentSize.Should().BeTrue();
        }

        [TestMethod]
        public void GivenTwoEqualByteArrays_WhenComparingByteArrays_Then_EqualFilesFlagShouldBeTrue()
        {
            var result = _diffService.CompareBytes(_byteArray1, _byteArray1);
            result.EqualFiles.Should().BeTrue();
        }

        [TestMethod]
        public void GivenTwoDifferentByteArrays_WhenComparingByteArrays_Then_EqualFilesFlagShouldBeFalse()
        {
            var result = _diffService.CompareBytes(_byteArray1, _byteArray2);
            result.EqualFiles.Should().BeFalse();
        }
    }
}
