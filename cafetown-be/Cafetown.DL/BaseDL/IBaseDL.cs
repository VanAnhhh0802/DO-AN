﻿using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="recordCode"></param>
        /// <param name="recordID"></param>
        /// <returns>bool kiểm tra có trùng hay không</returns>
        /// Modified by: TTTuan 5/1/2023
        public bool CheckDuplicateCode(Guid? recordID, string? recordCode);

        /// <summary>
        /// Xoá 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int DeleteRecordByID(Guid recordID);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int DeleteRecordsByIDs(string recordIDs);

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi trong bảng</returns>
        /// Modified by: TTTuan 5/1/2023
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Lấy ra mã bản ghi mới
        /// </summary>
        /// <returns>Mã bản ghi mới</returns>
        /// Modified by: TTTuan 5/1/2023
        public string GetNewCode();

        /// <summary>
        /// Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        public PagingResult<T> GetRecordsByFilter(string? keyword, int filter, int pageSize, int pageNumber);

        /// <summary>
        /// API lấy thông tin chi tiết của 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi</param>
        /// <returns>Thông tin của bản ghi theo ID</returns>
        /// Modified by: TTTuan 5/1/2023
        public  T GetRecordByID(Guid recordID);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="newRecord"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int InsertRecord(T newRecord);

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int UpdateRecordByID(Guid recordID, T record);
    }
}
