import department from "./department";
import employee from "./employee";
import inventory from "./inventory";
import invoice from "./invoice";
import authen from "./authen";
import inventoryItem from "./inventoryItem";
import stock from "./stock";
import table from "./table";
import vendor from "./vendor";
import ax from "axios";
import baseApi from "./base/base-api";

const axios = ax.create({
  // Khởi tạo cấu hình cho axios
  baseURL: process.env.VUE_APP_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});
const api = {
  // Khởi tạo các api
  department: department(axios),
  employee: employee(axios),
  inventory: inventory(axios),
  invoice: invoice(axios),
  authen: authen(axios),
  inventoryGroup: baseApi(axios, "inventorygroups"),
  inventoryItem: inventoryItem(axios),
  table: table(axios),
  vendor: vendor(axios),
  stock: stock(axios),
};

export default api;
