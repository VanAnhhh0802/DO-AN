<template>
    <div class="employee">
        <div class="employee-header">
            <div class="employee-title">{{ $t('stock_page.title') }}</div>
            <div style="display: flex; column-gap: 8px;">
                <v-button @click="handleAction(Enum.ACTION.ADD, null ,'out')">
                    {{ $t('stock_page.out_stock') }}
                </v-button>
                <v-button @click="handleAction(Enum.ACTION.ADD, null ,'add')">
                    {{ $t('stock_page.add_stock') }}
                </v-button>
            </div>
        </div>
        <div class="employee-body">
            <!-- Các action như search, export, reload -->
            <div class="employee-body__toolbar">
                <div class="employee-body__toolbar-left">
                    <slot name="toolbar-left">
                        <!--<div class="v-table__column-filter">
                            <div class="v-table__column-filter--text">
                                {{ $t("invoice_page.filter") }}: 
                            </div>
                        </div>
                         <div class="v-table__column-filter">
                            <div class="row sm-gutter flex--center">
                                <div class="col l-8 md-8">
                                    <v-combobox position="bottom" propKey="key" propValue="value" 
                                        v-model="selectedOptions" 
                                        :data="listOptions"
                                        :selectBox="true">
                                    </v-combobox>
                                </div>
                            </div>
                        </div> -->
                        <v-input :placeholder="$t('employee_page.search_function')" icon="ms-16 ms-icon ms-icon-search"
                            v-model="keyword" :outline="true" :styleProps="['width: 240px', 'font-style: italic']"
                            className="v-input__with-icon" :focus="true" 
                        />
                    </slot>
                </div>
                <div class="employee-body__toolbar-right">
                    <slot name="toolbar-right"></slot>
                    <div :tooltip="$t('action.reload_data')" class="ms-24 ms-icon ms-icon-reload ms-r-2 ml-l-2"
                        @click="handleAction(Enum.ACTION.RELOAD)">
                    </div>
                    <div :tooltip="$t('action.export_excel')" class="ms-24 ms-icon ms-icon-excel ms-x-2"
                        @click="handleAction(Enum.ACTION.EXPORT)"></div>
                </div>
            </div>
            <!-- Table hiển thị danh sách nhân viên -->
            <v-table v-model:columns="columns" :data="stockList.data" @action="handleAction" :actions="tableAction"
                :isDataLoaded="isDataLoaded">
            </v-table>
            <!-- Phân trang -->
            <v-pagination v-model:pageSize="pagination.pageSize" v-model:pageNumber="pagination.pageNumber"
                v-model:totalRecord="stockList.totalRecord">
            </v-pagination>

            <!-- Form sửa và thêm nhân viên -->
            <stock-form v-model="showInventoryItemForm" @insertInventoryItem="insertInventoryItem" @updateInventoryItem="updateEmployee" :isAdd="isAdd" >
            </stock-form>
            <!-- Khu vực hiển thị popup và toast thông báo -->
            <v-popup ref="popup"></v-popup>
            <v-toast ref="toast" :showProgress="true" :maxMessage="10"></v-toast>
        </div>
    </div>
</template>
<script>
import Enum from "@/utils/enum";
// import StockForm from './StockForm.vue';

// import { mapGetters } from 'vuex';

export default {
    components: { 
        // StockForm,
     },
    data() {
        return {
            keyword: "", // biến này dùng để lưu từ khóa tìm kiếm
            showInventoryItemForm: false, // biến này dùng để hiển thị form thêm mới hoặc cập nhật nhân viên
            stockList: [], // biến này dùng để lưu danh sách nhân viên
            pagination: {
                pageNumber: 1,
                pageSize: 20,
                keyword: "",
                filter: 2,
            }, // biến này dùng để lưu thông tin phân trang và tìm kiếm
            debounce: null, // biến này dùng để lưu hàm debounce,
            isDataLoaded: false, // biến này dùng để kiểm tra dữ liệu đã được load hay chưa
            listOptions: [
                {'key': 2, 'value': this.$t("filter.all")}, 
                {'key': 1, 'value': this.$t("filter.is_manager")}, 
                {'key': 0, 'value': this.$t("filter.is_employee")}
            ],
            selectedOptions: 2,
            isAdd: true,
        };
    },
    computed: {
        /**
         * @description: Lấy ra các action key (phím tắt) lưu trữ trong store 
         * Author: hvanh 11/10/2022
         */
        action: {
            get() {
                return this.$store.getters.getActionKey;
            },
            set(value) {
                this.$store.commit('setActionKey', value);
            }
        },
        /**
         * @description: Khai báo các cột hiển thị trong table
         * Author: hvanh 11/10/2022
         */
        columns: {
            get() {
                return [
                    {
                        title: this.$t(`stock_table.code`),
                        key: 'inventoryItemCode',
                        search: true,
                    },
                    {
                        title: this.$t(`stock_table.name`),
                        key: 'inventoryItemName',
                        width: "200px",
                        search: true,
                    },
                    {
                        title: this.$t(`stock_table.quatities_added`),
                        key: 'quantitiesAdd',
                    },
                    {
                        title: this.$t(`stock_table.UserName`),
                        key: 'userNameAdd',
                    },
                    {
                        title: this.$t(`stock_table.DateAdded`),
                        key: 'dateAdd',
                        textAlign: 'center',
                        type: 'date',
                    },
                    {
                        title: this.$t(`stock_table.action`),
                        key: 'action',
                        type: 'action',
                        fixed: true,
                        textAlign: 'center',
                        width: "120px",
                    },
                ];
            },
            set(value) {
                this.columns = value;
                console.log(this.columns);
            }
        },
        /**
         * @description: Khai báo các action thực hiện hàng loạt
         * Author: hvanh 11/10/2022
         */
        bathAction: {
            get() {
                return [{ 'key': Enum.ACTION.DELETE_MANY, 'value': this.$t('action.delete') }];
            }
        },
        /**
         * @description: Khai báo các action thực hiện trên từng dòng của table
         * Author: hvanh 11/10/2022
         */
        tableAction: {
            get() {
                return [
                    {
                        'key': Enum.ACTION.DUPLICATE,
                        'value': this.$t('action.duplicate'),
                    },
                    {
                        'key': Enum.ACTION.DELETE,
                        'value': this.$t('action.delete'),
                    }
                ]; // Khởi tạo danh sách action trên từng dòng
            }
        },

        employeesSelectedByID: {
            get() {
                return this.$store.getters.getListIdSelected;
            },
        }
    },
    watch: {
        selectedOptions: {
            handler: function (newVal) {
                this.pagination.filter = newVal;
                this.getStockList();
                console.log(newVal + ': ', this.stockList);
            },
            deep: true,
        },

        employeesSelectedByID: {
            handler: function (newVal) {
                this.$store.commit('setEmployeeSelected', newVal);
            },
            deep: true,
        },

        /**
         * @description: Lắng nghe sự thay đổi khi người dùng thay đổi liên quan tới phân trang
         * Author: hvanh 06/10/2022
         */
        pagination: {
            handler: function (newVal) {
                this.getEmployeeList(newVal);
            },
            deep: true,
        },
        /**
         * @description: Lắng nghe sự thay đổi khi người dùng thay đổi liên quan tới từ khóa tìm kiếm
         * Author: hvanh 06/10/2022
         */
        keyword: {
            /**
            * @description: Dùng debounce để tránh việc gọi api liên tục khi người dùng nhập từ khóa
            * Author: hvanh 23/09/2022
            */
            handler: function (newVal) {
                const me = this;
                clearTimeout(me.debounce);
                if (newVal) {
                    me.debounce = setTimeout(() => {
                        me.pagination.keyword = newVal.trim();
                    }, 500); // triển khai debounce để giảm số lần gọi api
                } else {
                    me.debounce = setTimeout(() => {
                        me.pagination.keyword = "";
                    }, 500); // triển khai debounce để giảm số lần gọi api
                }

            },
        },
        /**
         * @description: Lắng nghe sự kiện khi người dùng thay đổi trạng thái của form thêm mới hoặc cập nhật nhân viên bằng phím tắt
         * Author: hvanh 10/10/2022
         */
        action: {
            handler: function (newVal, type ) {
                // let curentFocus = document.activeElement; // lấy phần tử đang được active
                switch (newVal) {
                    case Enum.ACTION.ADD: // tổ hợp phím  Ctrl + Alt + A để thêm mới nhân viên
                    this.handleAction(newVal, null, type);
                        break;
                    case Enum.ACTION.DELETE: // phím delete để xóa nhân viên
                        if (this.isEmployeeSelected) {
                            this.handleAction(Enum.ACTION.DELETE_MANY);
                        }
                        break;
                    case Enum.ACTION.EXPORT:
                        this.handleAction(Enum.ACTION.EXPORT);
                        break;
                    case Enum.ACTION.RELOAD: // tổ hợp phím Ctrl + R để reload lại table
                        this.handleAction(Enum.ACTION.RELOAD);
                        break;
                    default:
                        break;
                }
            },
            deep: true,
        }
    },

    methods: {
        /**
         * @description: Thực hiện các action như thêm mới, sửa, xóa, xóa nhiều, tải lại dữ liệu, xuất ra excel
         * @param {string} action - Tên của action
         * @param {object} data - Dữ liệu của action
         * Author: hvanh 16/09/2022
         */
        handleAction(action, data, type) {
            const me = this;
            try {
                switch (action) {
                    case Enum.ACTION.ADD: // thêm mới nhân viên
                        if(type === 'add'){
                            me.showAddInventoryItemForm();
                        }
                        else if (type === 'out'){
                            me.showOutInventoryItemForm();
                        }
                        break;
                    case Enum.ACTION.EDIT: // sửa nhân viên
                        me.showEditEmployeeForm(data);
                        break;
                    case Enum.ACTION.DELETE: // xóa nhân viên
                        me.deleteEmployee(data);
                        break;
                    case Enum.ACTION.DELETE_MANY: // xóa nhiều nhân viên
                        me.deleteManyEmployee(data);
                        break;
                    case Enum.ACTION.DUPLICATE: // Nhân bản nhân viên
                        me.duplicateEmployee(data);
                        break;
                    case Enum.ACTION.INACTIVE:
                        me.$root.$toast.info(me.$t('notice_message.developing'));
                        break;
                    case Enum.ACTION.RELOAD: // Tải lại danh sách nhân viên
                        me.reloadData();
                        break;
                    case Enum.ACTION.EXPORT: // Xuất file excel
                        me.exportExcel();
                        break;
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để hiển thị form thêm mới nhân viên
         * Author: hvanh 07/10/2022
         */
        showAddInventoryItemForm() {
            const me = this;
            me.$store.dispatch('setMode', Enum.FORM_MODE.ADD);
            me.isAdd = true;
            me.showInventoryItemForm = true;
        },

        showOutInventoryItemForm() {
            const me = this;
            me.$store.dispatch('setMode', Enum.FORM_MODE.ADD);
            me.isAdd = false;
            me.showInventoryItemForm = true;
        },
        /**
         * @description: Hàm này dùng để sửa nhân viên
         * @param {object} employee - Dữ liệu của nhân viên
         * Author: hvanh 07/10/2022
         */
        showEditEmployeeForm(employee) {
            const me = this;
            me.$store.dispatch('setMode', Enum.FORM_MODE.EDIT);
            me.$store.dispatch('setEmployeeId', employee.employeeID);
            me.showInventoryItemForm = true;
        },
        /**
         * @description: Hàm này dùng để xóa nhân viên
         * @param {object} employee - Dữ liệu của nhân viên
         * Author: hvanh 07/10/2022
         */
        deleteEmployee(employee) {
            const me = this;
            me.deleteEmployeeBackend(employee);
        },
        /**
         * @description: Hàm này dùng để xóa nhiều nhân viên
         * Author: hvanh 1/3/2023
         */
        async deleteManyEmployee() {
            const me = this;
            const confirm = await me.$refs.popup.show({
                message: me.$t('notice_message.confirm_delete_many'),
                icon: Enum.ICON.WARNING,
                okButton: me.$t('confirm_popup.yes'),
                closeButton: me.$t('confirm_popup.cancel'),
            });
            if (confirm == me.$t('confirm_popup.yes')) {
                const result = await me.$store.dispatch('deleteMultipleEmployee');
                if (result != null) {
                    this.$store.commit('setListIdSelected', []);
                    me.$root.$toast.success(me.$t('notice_message.delete_many_success'));
                    me.getStockList();
                } else {
                    me.$root.$toast.error(me.$t('notice_message.delete_many_fail'));
                }
            }
        },
        /**
         * @description: Hàm này dùng để nhân bản nhân viên
         * @param {object} employee - Dữ liệu của nhân viên
         * Author: hvanh 07/10/2022
         */
        async duplicateEmployee(employee) {
            const me = this;
            me.$store.dispatch('setMode', Enum.FORM_MODE.DUPLICATE);
            me.$store.dispatch('setEmployeeId', employee.employeeID);
            me.showInventoryItemForm = true;
        },
        /**
         * @description: Hàm này dùng để tải lại danh sách nhân viên
         * Author: hvanh 07/10/2022
         */
        async reloadData() {
            const me = this;
            const res = await me.getStockList();
            if (res) {
                me.$root.$toast.success(me.$t('notice_message.reload_data_success'));
            } else {
                me.$root.$toast.error(me.$t('notice_message.reload_data_fail'));
            }
        },
        /**
         * @description: Hàm này dùng để xuất danh sách nhân viên ra file excel
         * Author: hvanh 05/10/2022
         */
        async exportExcel() {
            const me = this;
            try {
                me.$root.$toast.info(me.$t('notice_message.export_excel_processing'));
                const res = await me.$api.employee.exportEmployees(me.pagination); // kiểm tra xem có dữ liệu không
                if (res.status == Enum.MISA_CODE.SUCCESS) {
                    const link = document.createElement('a'); // tạo thẻ a để download file
                    link.href = res.request.responseURL; // đường dẫn tải file
                    link.click();
                    me.$root.$toast.success(me.$t('notice_message.export_excel_success'));
                }
            } catch (error) {
                me.$root.$toast.error(me.$t('notice_message.export_excel_fail'));
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để xóa nhân viên ở bên backend
         * @param {object} data - Dữ liệu của nhân viên cần xóa
         * Author: hvanh 19/09/2022
         */
        async deleteEmployeeBackend(employee) {
            const me = this;
            try {
                const confirm = await me.$refs.popup.show({
                    message: me.$t('notice_message.confirm_delete', [employee.employeeCode]),
                    icon: Enum.ICON.WARNING,
                    okButton: me.$t('confirm_popup.yes'),
                    closeButton: me.$t('confirm_popup.cancel'),
                });
                if (confirm == me.$t('confirm_popup.yes')) {
                    const res = await me.$api.employee.deleteEmployee(employee.employeeID);
                    if (res.status == Enum.MISA_CODE.SUCCESS) {
                        me.deleteEmployeeFrontEnd(employee);
                    } else {
                        me.$root.$toast.error(me.$t('notice_message.delete_fail', [employee.employeeCode]));
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để xóa nhân viên trong danh sách ở bên frontend
         * @param: {object} data - Dữ liệu của nhân viên cần xóa 
         * Author: hvanh 22/09/2022
         */
        deleteEmployeeFrontEnd(data) {
            const me = this;
            const { employeeID, employeeCode } = data;
            try {
                const index = me.stockList.data.findIndex((item) => item.employeeID === employeeID);
                if (index !== -1) {
                    me.stockList.data.splice(index, 1);
                    me.$root.$toast.success(me.$t('notice_message.delete_success', [employeeCode]));
                    me.stockList.totalRecord -= 1; // Giảm tổng số bản ghi đi 1
                }
            } catch (error) {
                me.$root.$toast.success(me.$t('notice_message.delete_fail', [employeeCode]));
                console.log(error);
            }
        },
        /**
         * @description: Hàm này đùng để nhận emit từ con nếu thành công thì thêm nhân viên vào bảng 
         * @param: {Object} employee - Dữ liệu của nhân viên
         * Author: hvanh 01/10/2022
         */
        insertInventoryItem(employee) {
            const me = this;
            try {
                me.stockList.data.unshift(employee); // Thêm nhân viên vào đầu mảng
                me.$root.$toast.success(me.$t('notice_message.insert_success', [employee.employeeCode]));
                me.stockList.totalRecord += 1; // Tăng tổng số bản ghi lên 1
            } catch (error) {
                me.$root.$toast.error(me.$t('notice_message.insert_fail', [employee.employeeCode]));
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để cập nhật nhân viên ở bên frontend
         * Author: hvanh 05/10/2022
         */
        updateEmployee(employee) {
            const me = this;
            try {
                const index = me.stockList.data.findIndex((item) => item.employeeID === employee.employeeID);
                if (index !== -1) {
                    me.stockList.data.splice(index, 1);
                    me.stockList.data.unshift(employee);
                    me.$root.$toast.success(me.$t('notice_message.update_success', [employee.employeeCode]));
                }
            } catch (error) {
                me.$root.$toast.error(me.$t('notice_message.update_fail', [employee.employeeCode]));
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để lấy danh sách nhân viên
         * Author: hvanh 19/09/2022
         */
        async getStockList() {
            const me = this;
            try {
                me.isDataLoaded = false;
                const result = await me.$api.vendor.getTableFilter(me.pagination);
                if (result.status == Enum.MISA_CODE.SUCCESS) {
                    me.stockList = result.data;
                    me.isDataLoaded = true;
                    return Promise.resolve(true);
                } else {
                    return Promise.resolve(false);
                }
            } catch (error) {
                me.$refs.popup.showError(me.$t('notice_message.get_employee_list_fail'));
                console.log(error);
                me.isDataLoaded = false;
                return Promise.reject(false);
            }
        },

    },
    created() { // Hàm này chạy khi component được tạo
        const me = this;
        me.getStockList(); // Lấy danh sách nhân viên
        me.Enum = Enum; // Khởi tạo enum
    },

    updated() {
        let me = this;
        me.employeesSelectedByID = this.$store.getters.getListIdSelected;

        me.listOptions = [
            {'key': 2, 'value': this.$t("filter.all")}, 
            {'key': 1, 'value': this.$t("filter.is_manager")}, 
            {'key': 0, 'value': this.$t("filter.is_employee")}
        ];
    },
}
</script>
<style lang="scss" scoped>
.employee {
    width: 100%;
    display: flex;
    flex: 1;
    flex-direction: column;

    &-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 12px;
        box-sizing: border-box;
    }

    &-title {
        font-size: 24px;
        font-family: "MISA Fonts Bold";
    }

    &-body {
        background-color: #fff;
        box-sizing: border-box;
        flex: 1;
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;

        &__toolbar {
            display: flex;
            justify-content: space-between;
            padding: 16px;
            margin-bottom: 16px;
            margin-top: 20px;

            &-left {
                display: flex;
                align-items: center;
            }

            &-right {
                display: flex;
                align-items: center;
            }
        }
    }
}

.v-table__column-filter--text {
    font-weight: 700;
    margin-right: 12px;
    font-family: "MISA Fonts Bold";
    font-size: 14px;
    margin-left: 24px;
}
</style>