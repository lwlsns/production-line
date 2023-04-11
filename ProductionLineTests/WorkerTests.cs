namespace ProductionLineTests
{
    [TestClass]
    public class WorkerTests
    {
        private readonly ConveyorBelt belt;
        int beltLength = 5;

        public WorkerTests()
        {
            //we can't write tests that rely on random numbers, so we'll use a fixed generator
            IRandomGenerator randomGenerator = new FixedRandomGenerator(0,1,2);
            belt = new ConveyorBelt(beltLength, randomGenerator);
        }

        [TestMethod]
        public void MoveBelt_GeneratesFixedComponents()
        {
            // Arrange

            // Act
            belt.MoveBelt();
            char?[] expectedBelt1 = { 'A', null, null, null, null };
            CollectionAssert.AreEqual(expectedBelt1, belt.GetBelt());

            belt.MoveBelt();
            char?[] expectedBelt2 = { 'B', 'A', null, null, null };
            CollectionAssert.AreEqual(expectedBelt2, belt.GetBelt());

            belt.MoveBelt();
            char?[] expectedBelt3 = { null, 'B', 'A', null, null };
            CollectionAssert.AreEqual(expectedBelt3, belt.GetBelt());

            belt.MoveBelt();
            char?[] expectedBelt4 = { 'A', null, 'B', 'A', null};
            CollectionAssert.AreEqual(expectedBelt4, belt.GetBelt());
        }


        [TestMethod]
        public void CanPick_BothHandsEmpty_ReturnsTrue()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            //Act
            bool canPickUpComponent = worker.CanPickUpComponent();
            //Assert
            Assert.IsTrue(canPickUpComponent);
        }

        [TestMethod]
        public void CanPick_OneHandEmpty_ReturnsTrue()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            worker.PickUpComponent();
            belt.MoveBelt();
            //Act
            bool canPickUpComponent = worker.CanPickUpComponent();
            //Assert
            Assert.IsTrue(canPickUpComponent);
        }

        //[TestMethod]
        //public void CanPick_BothHandsFull_ReturnsFalse()
        //{
        //    //Arrange
        //    Worker worker = new Worker(belt, 0, 1);
        //    belt.PutItem(0, 'A');
        //    worker.PickUpComponent();
        //    belt.PutItem(0, 'B');
        //    worker.PickUpComponent();
        //    //Act
        //    bool canPickUpComponent = worker.CanPickUpComponent();
        //    //Assert
        //    Assert.IsFalse(canPickUpComponent);
        //}

        //[TestMethod]
        //public void CanPick_HandsHaveSameComponent_ReturnsFalse()
        //{
        //    //Arrange
        //    Worker worker = new Worker(belt, 0, 1);
        //    belt.PutItem(0, 'A');
        //    worker.PickUpComponent();
        //    belt.PutItem(0, 'A');
        //    //Act
        //    bool canPickUpComponent = worker.CanPickUpComponent();
        //    //Assert
        //    Assert.IsFalse(canPickUpComponent);
        //}

        [TestMethod] 
        public void PickUpComponent_PicksUpComponentFromBeltAndPlacesItInHand()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            //Act
            worker.PickUpComponent();
            //Assert
            Assert.IsTrue(worker.Hands.Contains('A'));
        }

        [TestMethod]
        public void Update_PicksUpComponentFromBeltAndPlacesItInHand()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            //Act
            worker.Update();
            //Assert
            Assert.IsTrue(worker.Hands.Contains('A'));
        }

        [TestMethod]
        public void Update_PicksUpTwoComponentsFromBeltAndPlacesItInHand()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            //Act
            worker.Update();
            belt.MoveBelt();
            worker.Update();
            //Assert
            Assert.IsTrue(worker.Hands.Contains('A'));
            Assert.IsTrue(worker.Hands.Contains('B'));
        }

        [TestMethod]
        public void CanAssembleProduct_HandsHaveAAndB_ReturnsTrue()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            worker.PickUpComponent();
            belt.MoveBelt();
            worker.PickUpComponent();
            //Act
            bool canAssembleProduct = worker.CanAssembleProduct();
            //Assert
            Assert.IsTrue(canAssembleProduct);
        }

        [TestMethod]
        public void CanAssembleProduct_HandsHaveA_ReturnsFalse()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            worker.PickUpComponent();
            //Act
            bool canAssembleProduct = worker.CanAssembleProduct();
            //Assert
            Assert.IsFalse(canAssembleProduct);
        }

        [TestMethod]
        public void CanAssembleProduct_HandsHaveB_ReturnsFalse()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            belt.MoveBelt();
            worker.PickUpComponent();
            //Act
            bool canAssembleProduct = worker.CanAssembleProduct();
            //Assert
            Assert.IsFalse(canAssembleProduct);
        }

        //test can assemble product imediately after assembling product
        [TestMethod]
        public void CanAssembleProduct_AfterAssemblingProduct_ReturnsFalse()
        {
            //Arrange
            Worker worker = new Worker(belt, 0, 1);
            belt.MoveBelt();
            worker.PickUpComponent();
            belt.MoveBelt();
            worker.PickUpComponent();
            worker.AssembleProduct();
            //Act
            bool canAssembleProduct = worker.CanAssembleProduct();
            //Assert
            Assert.IsFalse(canAssembleProduct);
        }
    }
}