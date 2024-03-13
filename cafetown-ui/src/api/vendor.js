const PREFIX_EMPLOYEE = "Vendors";
export default (axios) => ({
  /**
   * @description: Lấy chi tiết mặt hàng
   * @param {String} id Id nhân viên
   * @return {Promise} Promise
   * Author: hvanh 1/3/2023
   */
  getVendorById(id) {
    return axios.get(`${PREFIX_EMPLOYEE}/${id}`);
  },

  /**
   * @description: Lấy danh sách nhân viên theo filter
   * @param {Object} filter Đối tượng filter
   * @return {Promise} Promise
   * Author: hvanh 1/3/2023
   */
  getVendorFilter(object) {
    var defaultObject = {
      pageNumber: 1,
      pageSize: 10,
      keyword: "",
      filter: 2,
    };
    object = Object.assign(defaultObject, object);
    return axios.get(`${PREFIX_EMPLOYEE}/filter?keyword=${object.keyword}&filter=${object.filter}&pageSize=${object.pageSize}&pageNumber=${object.pageNumber}`, {
    });
  },

  /**
   * @description: Xóa nhân viên
   * @param {String} employeeId Id nhân viên
   * @return {Promise} Promise
   * Author: hvanh 1/3/2023
   */
  deleteVendor(id) {
    return axios.delete(`${PREFIX_EMPLOYEE}/${id}`);
  },

  /**
   * @description: Thêm mới nhân viên
   * @param {Object} employee Đối tượng nhân viên
   * @return {Promise} Promise
   * Author: hvanh 1/3/2023
   */
  insertVendor(object) {
    return axios.post(`${PREFIX_EMPLOYEE}`,object);
  },

  /**
   * @description: Cập nhật nhân viên
   * @param {Object} employee Đối tượng nhân viên
   * @return {Promise} Promise
   * Author: hvanh 1/3/2023
   */
  updateVendor(object) {
    return axios.put(`${PREFIX_EMPLOYEE}/${object.vendorID}`, object);
  },
  exportVendors(object) {
    return axios.get(`${PREFIX_EMPLOYEE}/export?keyword=${object.keyword}`, {
      responseType: "blob",
    });
  },
});