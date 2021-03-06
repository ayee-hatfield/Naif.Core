﻿//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System.Collections.Generic;
using System.Linq;
using Naif.Core.Collections;
using NUnit.Framework;

namespace Naif.Core.Tests
{
    [TestFixture]
    public class PagingExtensionsTests
    {
        [Test]
        public void PagingExtensions_InPagesOf_Returns_PageSelector()
        {
            //Arrange
            IQueryable<int> queryable = Util.CreateIntegerList(Constants.PAGE_TotalCount).AsQueryable();

            //Act
            PageSelector<int> pageSelector = queryable.InPagesOf(Constants.PAGE_RecordCount);

            //Assert
            Assert.IsInstanceOf<PageSelector<int>>(pageSelector);
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(0, 6)]
        [TestCase(2, 4)]
        [TestCase(4, 4)]
        public void PagingExtensions_ToPagedList_Returns_PagedList_From_Enumerable(int index, int pageSize)
        {
            //Arrange
            List<int> enumerable = Util.CreateIntegerList(Constants.PAGE_TotalCount);

            //Act
            IPagedList<int> pagedList = enumerable.ToPagedList(index, pageSize);

            //Assert
            Assert.IsInstanceOf<IPagedList<int>>(pagedList);
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(0, 6)]
        [TestCase(2, 4)]
        [TestCase(4, 4)]
        public void PagingExtensions_ToPagedList_Returns_PagedList_From_Enumerable_With_Correct_Index_AndPageSize(
            int index, int pageSize)
        {
            //Arrange
            List<int> enumerable = Util.CreateIntegerList(Constants.PAGE_TotalCount);

            //Act
            IPagedList<int> pagedList = enumerable.ToPagedList(index, pageSize);

            //Assert
            Assert.AreEqual(index, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(0, 6)]
        [TestCase(2, 4)]
        [TestCase(4, 4)]
        public void PagingExtensions_ToPagedList_Returns_PagedList_From_Queryable(int index, int pageSize)
        {
            //Arrange
            IQueryable<int> queryable = Util.CreateIntegerList(Constants.PAGE_TotalCount).AsQueryable();

            //Act
            IPagedList<int> pagedList = queryable.ToPagedList(index, pageSize);

            //Assert
            Assert.IsInstanceOf<IPagedList<int>>(pagedList);
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(0, 6)]
        [TestCase(2, 4)]
        [TestCase(4, 4)]
        public void PagingExtensions_ToPagedList_Returns_PagedList_From_Queryable_With_Correct_Index_AndPageSize(
            int index, int pageSize)
        {
            //Arrange
            IQueryable<int> queryable = Util.CreateIntegerList(Constants.PAGE_TotalCount).AsQueryable();

            //Act
            IPagedList<int> pagedList = queryable.ToPagedList(index, pageSize);

            //Assert
            Assert.AreEqual(index, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }
    }

}
