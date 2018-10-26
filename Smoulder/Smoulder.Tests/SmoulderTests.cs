using System.Threading.Tasks;
using Smoulder.Tests.TestConcreteClasses;
using Xunit;

namespace Smoulder.Tests
{
    public class SmoulderTests
    {
        [Fact]
        public void CanCreateSmoulder()
        {
            //Arrange
            var smoulderFactory = new SmoulderFactory();

            //Act
            var smoulder = smoulderFactory.Build(new LoaderTest(), new ProcessorTest(), new DistributorTest());

            //Assert
            Assert.NotNull(smoulder);
        }

        [Fact]
        public void CanStartAndStopSmoulder()
        {
            //Arrange
            var smoulderFactory = new SmoulderFactory();
            var smoulder = smoulderFactory.Build(new LoaderTest(), new ProcessorTest(), new DistributorTest());

            //Act
            smoulder.Start();

            System.Threading.Thread.Sleep(100);

            smoulder.Stop();

            //Assert
            Assert.NotNull(smoulder);
        }
    }
}
