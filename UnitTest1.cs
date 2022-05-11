using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {

        [Test]
        public void Test_emptyConstructor()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act

            //Assert
            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
           // Assert.AreEqual(nums.ToString(), Is.EqualTo("[]"));   
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Arrange
            var nums = new Collection<int>(5);

            //Act

            //Assert
           Assert.That(nums.ToString(), Is.EqualTo("[5]"));
        }

        [Test]

        public void Test_Collection_Add()
        {
            var nums = new Collection<int>(5);
            nums.Add(6);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 6]"));

        }

        [Test]
        public void TestCollectionConstructorMultipleItems()
        {
            var nums = new Collection<int>(5, 10, 81);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 81]"));
        }
        
        [Test]

        public void TestCollectionsAdd()
        {
         
            var nums = new Collection<int>();

           
            nums.Add(10);
            nums.Add(30);

           
            Assert.That(nums.ToString(), Is.EqualTo("[10, 30]"));
        }

        [Test]

        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void TestCollectionGetByIndex()
        {
           
            var names = new Collection<string>("Petya", "Georgi");
            var item0 = names[0];
            var item1 = names[1];
            
            Assert.That(item0, Is.EqualTo("Petya"));
            Assert.That(item1, Is.EqualTo("Georgi"));
        }

        [Test]
        public void TestCollectionGetByInvalidIndex()
        {
            var names = new Collection<string>("Petya", "Georgi");

            Assert.That(() => { var name = names[-1]; },
            Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[2]; },
            Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[500]; },
            Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(names.ToString(), Is.EqualTo("[Petya, Georgi]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Petya", "Georgi");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            Assert.That(nestedToString,
              Is.EqualTo("[[Petya, Georgi], [10, 20], []]"));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

    }
}