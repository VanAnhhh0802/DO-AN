/**
 * @description: Tạo store lưu trữ các mode của dialog (thêm, sửa, xóa)
 * Author: hvanh 5/3/2023
 */
const dialogMode = {
  state: {
    mode: null, // mode của dialog (thêm, sửa, xóa)
    action: null, // action của dialog (thêm, sửa, xóa)
    employeeId: null, // id của nhân viên
    vendorId: null, // id của nhân viên
    tablesId: null, // id của nhân viên
    inventoryId: null, // id của hàng hóa
    invoiceId: null, // id của hóa đơn
    informationId: null, // id thông tin cá nhân
  },
  mutations: {
    /**
     * @description: Thay đổi mode của dialog (Thêm, sửa, xóa)
     * Author: hvanh 5/3/2023
     */
    setMode(state, mode) {
      state.mode = mode;
    },
    /**
     * @description: Thay đổi id của nhân viên
     * Author: hvanh 5/3/2023
     */
    setEmployeeId(state, employeeId) {
      state.employeeId = employeeId;
    },
    setVendorId(state, vendorId) {
      state.vendorId = vendorId;
    },
    setTableId(state, tablesId) {
      state.tablesId = tablesId;
    },
    /**
     * @description: Thay đổi id của hàng hía
     * Author: hvanh 5/3/2023
     */
    setInventoryId(state, inventoryId) {
      state.inventoryId = inventoryId;
    },
    /**
     * @description: Thay đổi id của hàng hía
     * Author: hvanh 5/3/2023
     */
    setInvoiceId(state, invoiceId) {
      state.invoiceId = invoiceId;
    },
        /**
     * @description: Thay đổi id của hàng hía
     * Author: hvanh 5/3/2023
     */
    setInformationId(state, informationId) {
      state.informationId = informationId;
    },
    /**
     * @description: TThay đổi action trong dialog (Cất, Cất và tiếp tục, Hủy)
     * Author: hvanh 5/3/2023
     */
    setAction(state, action) {
      state.action = action;
    },
  },
  actions: {
    /**
     * @description: Thay đổi mode của dialog (Thêm, sửa, xóa)
     * Author: hvanh 5/3/2023
     */
    setMode({ commit }, mode) {
      commit("setMode", mode);
    },
    /**
     * @description: Thay đổi id của nhân viên
     * Author: hvanh 5/3/2023
     */
    setEmployeeId({ commit }, employeeId) {
      commit("setEmployeeId", employeeId);
    },
    setVendorId({ commit }, vendorId) {
      commit("setVendorId", vendorId);
    },
    setTableId({ commit }, tablesId) {
      commit("setTableId", tablesId);
    },
    /**
     * @description: Thay đổi id của hàng hóa
     * Author: hvanh 5/3/2023
     */
    setInventoryId({ commit }, inventoryId) {
      commit("setInventoryId", inventoryId);
    },
    /**
     * @description: Thay đổi id của hàng hóa
     * Author: hvanh 5/3/2023
     */
    setInvoiceId({ commit }, invoiceId) {
      commit("setInvoiceId", invoiceId);
    },
    /**
     * @description: Thay đổi id của hàng hóa
     * Author: hvanh 5/3/2023
     */
    setInformationId({ commit }, informationId) {
      commit("setInformationId", informationId);
    },
    /**
     * @description: Thay đổi action trong dialog (Cất, Cất và tiếp tục, Hủy)
     * Author: hvanh 5/3/2023
     */
    setAction({ commit }, action) {
      commit("setAction", action);
    },
  },
  getters: {
    /**
     * @description: Lấy mode của dialog (Thêm, sửa, xóa)
     * Author: hvanh 5/3/2023
     */
    getMode(state) {
      return state.mode;
    },
    /**
     * @description: Lấy id của nhân viên trong dialog
     * Author: hvanh 5/3/2023
     */
    getEmployeeId(state) {
      return state.employeeId;
    },
    getVendorId(state) {
      return state.vendorId;
    },
    getTableId(state) {
      return state.tablesId;
    },
    /**
     * @description: Lấy id của hàng hóa trong dialog
     * Author: hvanh 5/3/2023
     */
    getInventoryId(state) {
      return state.inventoryId;
    },
    /**
     * @description: Lấy id của hàng hóa trong dialog
     * Author: hvanh 5/3/2023
     */
    getInvoiceId(state) {
      return state.invoiceId;
    },
    /**
     * @description: Lấy id của hàng hóa trong dialog
     * Author: hvanh 5/3/2023
     */
    getInformationId(state) {
      return state.informationId;
    },
    /**
     * @description: Lấy action trong dialog (Cất, Cất và tiếp tục, Hủy)
     * Author: hvanh 5/3/2023
     */
    getAction(state) {
      return state.action;
    },
  },
};

export default dialogMode;
