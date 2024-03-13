import { createRouter, createWebHistory } from "vue-router";

function lazyLoad(view) {
  return () => import(`@/views/${view}.vue`);
}
// Khai báo các routes
const router = [
  {
    path: "/",
    redirect: "/dang-nhap",
    
  },
  {
    path: '/dang-nhap',
    name: "auth-login",
    component: lazyLoad("login/Login"),
    meta: {
      auth: true,
      title: "Đăng nhập",
    },
  },
  {
    path: "/tong-quan",
    component: lazyLoad("overview/OverviewDashboard"),
    meta: {
      title: "Tổng quan",
    },
    sideBar: {
      icon: "ms-icon ms-icon-dashboard",
      title: "overview",
      order: 1,
    },
  },
  {
    path: "/nhan-vien",
    component: lazyLoad("employee/EmployeeList"),
    meta: {
      title: "Nhân viên",
    },
    sideBar: {
      icon: "ms-icon ms-icon-employee",
      title: "employee",
      order: 2,
    },
  },
  {
    path: "/hoa-don-ban-hang",
    component: lazyLoad("invoice/InvoiceList"),
    meta: {
      title: "Hóa đơn bán hàng",
    },
    sideBar: {
      icon: "ms-icon ms-icon-bill",
      title: "manage_invoice",
      order: 6,
    },
  },
  {
    path: "/quan-ly-hang-hoa",
    component: lazyLoad("inventory/InventoryList"),
    meta: {
      title: "Quản lý hàng hóa",
    },
    sideBar: {
      icon: "ms-icon ms-icon-warehouse",
      title: "manage_inventory",
      order: 7,
    },
  },
  // {
  //   path: "/kho",
  //   component: lazyLoad("warehouse/stock/StockPage"),
  //   meta: {
  //     title: "Quản lý kho",
  //   },
  //   sideBar: {
  //     icon: "ms-icon ms-icon-warehouse-stock",
  //     title: "manage_stock",
  //     order: 8,
  //   },
  // },
  {
    path: "/ban",
    component: lazyLoad("TableManager/TableList"),
    meta: {
      title: "Quản lý bàn",
    },
    sideBar: {
      icon: "ms-icon ms-icon-buy",
      title: "manage_table",
      order: 9,
    },
  },
  {
    path: "/vendor",
    component: lazyLoad("Vendor/VendorList"),
    meta: {
      title: "Quản lý nhà cung cấp",
    },
    sideBar: {
      icon: "ms-icon ms-icon-bank",
      title: "manage_vendor",
      order: 10,
    },
  },
];

// Khởi tạo router
const routes = createRouter({
  history: createWebHistory(),
  routes: router,
});

// Cập nhật meta title cho từng route
routes.beforeEach((to, from, next) => {
  document.title = to.meta.title;
  next();
});

export default routes;
