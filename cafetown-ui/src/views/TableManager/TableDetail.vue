<template>
    <div>
    <!-- Khu vực hiển thị dialog form thêm hoặc sửa nhân viên -->
    <v-dialog :isShow="modelValue" @close="closeFormHandle">
            <template #title>
                <div class="row e-header">
                    <div class="e-header__title col font-weight-700">
                        {{ isAdd ? $t('table_detail.title_add') :  $t('table_detail.title_edit')}}
                    </div>
                </div>
            </template>
            <template #body>
                <div class="grid wide v-max-900" ref="inventoryItemForm">
                        <div class="col l-6 md-6">
                            <div class="row sm-gutter">
                                <div class="form-group col l-5 md-5 c-5 focus">
                                    <v-input :label="$t('table_detail.code')" v-model="tableManger.tablesCode"
                                        ref="TablesCode" @validate="setValid('TablesCode', $event)" :maxLength="100"    
                                        :required="true" :errorLabel="$t('table_detail.code')">
                                    </v-input>
                                </div>
                                <div class="form-group col l-7 md-7 c-7">
                                    <v-input :label="$t('table_detail.name')" v-model="tableManger.tablesName"    
                                        ref="TablesName" @validate="setValid('TablesName', $event)" :maxLength="100"
                                        :required="true" :errorLabel="$t('table_detail.name')">
                                    </v-input>
                                </div>
                                <div class="form-group col l-12 md-12">
                                    <v-input 
                                        :label="$t('table_detail.capcity')" 
                                        v-model="tableManger.capacity">
                                    </v-input>
                                </div>
                            </div>
                        </div>
                </div>
            </template>
            <template #footer__left>
                <v-button buttonType="secondary" @click="closeFormHandle" className="v-button__button-no-bg border">
                    {{ $t('action_form.cancel') }}
                </v-button>
                <div style="max-width: 0; max-height: 0; overflow: hidden;">
                    <input @focus="inputFocus()" />
                </div>
            </template>
            <template #footer__right>
                <v-button @click="saveHandler(Enum.ACTION.SAVE_AND_CLOSE)" buttonType="secondary"
                    :tooltip="$t('action_form.save') + Enum.KEY_DEFINE.CTRL_S">
                    {{ $t('action_form.save') }}
                </v-button>
                <v-button @click="saveHandler(Enum.ACTION.SAVE_AND_ADD)"
                    :tooltip="$t('action_form.save_and_add') + Enum.KEY_DEFINE.CTRL_SHIFT_S">
                    {{ $t('action_form.save_and_add') }}
                </v-button>
            </template>
        </v-dialog>
        <!-- Khu vực hiển thị popup cảnh báo -->
        <v-popup ref="popup" :tabIndex="0"></v-popup>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';
import Enum from "@/utils/enum";

export default {
    name: "TableDetail",
    props: {
        modelValue: { // dùng để đóng mở form
            type: Boolean,
            default: false
        },
        isAdd : { 
            type: Boolean,
            default: true
        }
    },
    data() {
        return {
            tableManger: { // dữ liệu nhân viên
                tablesID: "",
                tablesCode: "",
                tablesName: "",
                capacity: 0,
            },
            attemptSubmit: true, // biến kiểm tra đã submit form chưa
            Enum: Enum, // dùng để gọi Enum trong template 
            isChaged: false, // dùng để check xem có thay đổi dữ liệu hay không
        };
    },
    computed: {
        ...mapGetters(["getTableId"]),

        checkDisableIsEmployee: {
            get() {
                if (this.tableManger.isManager == false && this.tableManger.isEmployee == true) {
                    return true;
                } else {
                    return false;
                }
            }
        },

        checkDisableIsManager: {
            get() {
                if (this.tableManger.isEmployee == false && this.tableManger.isManager == true) {
                    return true;
                } else {
                    return false;
                }
            }
        },

        /**
         * @description: Get và set trạng thái của form lưu trữ trong store 
         * Author: hvanh 08/10/2022
         */
        formMode: {
            set(val) {
                this.$store.dispatch('setMode', val);
            },
            get() {
                return this.$store.getters.getMode;
            },
        },
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
        }
    },
    watch: {
        /**
         * @description: Xử lý khi form được mở sau khi đóng thì set mode lại là null
         * Author: hvanh 08/10/2022
         */
        modelValue: {
            handler(val) {
                if (!val) {
                    this.formMode = Enum.FORM_MODE.NULL;
                }
            },
            deep: true
        },
        /**
         * @description: Nhận các mode từ bên component tableManger-list và xử lý các nghiệp vụ tương ứng
         * Author: hvanh 1/3/2023
         */
        formMode: {
            handler: async function (formMode) {
                const self = this;
                switch (formMode) {
                    case Enum.FORM_MODE.ADD:
                        await self.resetForm();
                        break;
                    case Enum.FORM_MODE.EDIT:
                        await self.getEmployeeById();
                        self.isChaged = false;
                        break;
                    case Enum.FORM_MODE.DUPLICATE:
                        await self.getEmployeeById(true);
                        break;
                    default:
                        break;
                }
                self.inputFocus();
            },
            deep: true,
        },
        /**
         * @description: Lấy ra các action key (phím tắt) và xử lý các nghiệp vụ tương ứng
         * Author: hvanh 1/3/2023
         */
        action: {
            handler: async function (action) {
                const self = this;
                if (self.formMode !== Enum.FORM_MODE.NULL) {
                    switch (action) {
                        case Enum.ACTION.CLOSE:
                            self.closeFormHandle();
                            break;
                        case Enum.ACTION.SAVE_AND_CLOSE:
                            self.saveHandler(action);
                            break;
                        case Enum.ACTION.SAVE_AND_ADD:
                            self.saveHandler(action);
                            break;
                        default:
                            break;
                    }
                }
            },
            deep: true,
        },
        /**
         * @description: Bắt sự thay đổi của tableManger nếu thay đổi thì set isChaged = true
         * Author: hvanh 1/3/2023
         */
        tableManger: {
            handler: function () {
                this.isChaged = true;
            },
            deep: true
        },
    },
    methods: {
        /**
         * @description: Hàm xử lý sự kiện đóng form nhân viên
         * @param {boolean} foceClose: có bắt buộc đóng form hay không
         * Author: hvanh 22/09/2022
         */
        async closeFormHandle(forceClose = false) {
            try {
                const self = this;
                if (forceClose || !self.isChaged) {
                    self.resetForm(false);
                    self.$emit("update:modelValue", false);
                    self.$store.dispatch("setMode", null);
                    return;
                }
                if (self.isChaged) {
                    const confirm = await self.$refs.popup.show({
                        message: self.$t("notice_message.confirm_data_close"),
                        icon: Enum.ICON.INFO,
                        okButton: self.$t("confirm_popup.yes"),
                        cancelButton: self.$t("confirm_popup.no"),
                        closeButton: self.$t("confirm_popup.cancel"),
                    }); // hiển thị popup cảnh báo
                    switch (confirm) {
                        case self.$t("confirm_popup.yes"):
                            self.saveHandler(Enum.ACTION.SAVE_AND_CLOSE);
                            break;
                        case self.$t("confirm_popup.no"):
                            self.$emit("update:modelValue", false);
                            break;
                        default:
                            break;
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để cập nhật nhân viên và emit giá trị tableManger vừa cập nhật
         * @param: {enum} action: hành động sau khi cập nhật
         * Author: hvanh 19/09/2022
         */
        async updateInventoryItem() {
            let self = this;
            const response = await self.$api.table.updateTable(self.tableManger); // gọi api update nhân viên
            if (response.status == Enum.MISA_CODE.SUCCESS) {
                self.$emit("updateInventoryItem", self.tableManger); // emit giá trị tableManger vừa cập nhật
                return Promise.resolve(true);
            }
        },
        /**
        * @description: Hàm này dùng để thêm mới nhân viên và emit giá trị tableManger vừa thêm mới
        * @param: {enum} action: hành động sau khi thêm mới
        * Author: hvanh 19/09/2022
        */
        async insertInventoryItem() {
            let self = this;
            const response = await self.$api.table.insertTable(self.tableManger);
            if (response.status == Enum.MISA_CODE.CREATED) {
                self.tableManger.inventoryItemID = response.data; // gán giá trị employeeID vừa thêm mới
                self.$emit("insertInventoryItem", self.tableManger); // emit giá trị tableManger vừa thêm mới
                return Promise.resolve(true);
            }
        },
        /**
         * @description: Hàm này dùng để reset form về giá trị mặc định
         * @param: {boolean} getNewEmployeeCode: có lấy mã nhân viên mới hay không
         * Author: hvanh 22/09/2022
         */
        async resetForm() {
            let self = this;
            self.attemptSubmit = false; // reset lại trạng thái submit
            self.tableManger = { // gán giá trị mặc định cho tableManger
                tablesCode: "",
                tablesName: "",
                capacity: 0,
            };
        },
        /**
         * @description: Hàm này dùng để lấy thông tin chi tiết nhân viên theo id và gán vào tableManger
         * @param {boolean} getNewEmployeeCode: có lấy mã nhân viên mới hay không ( phục vụ chức năng nhân bản)
         * Author: hvanh 22/09/2022
         */
        async getEmployeeById() {
            try {
                let self = this;
                const response = await self.$api.table.getTableById(self.getTableId);
                if (response.status == Enum.MISA_CODE.SUCCESS) {
                    self.tableManger = response.data;
                }
            } catch (error) {
                console.log(error);
            }
        },

        setValid(fieldName, valid) {
            console.log(fieldName, valid);
        },
        /**
         * @description: Hàm này dùng để xử lý sự kiện cất
         * @param: {string} action: hành động sau khi cất
         * Author: hvanh 19/09/2022
         */
        async saveHandler(action) {
            let self = this;
            try {
                self.attemptSubmit = true; // set trạng thái submit
                const validateResult = await self.$nextTick(async () => { // đợi cho khi các ô input validate xong thì mới tìm class error
                    const errorClass = self.$el.querySelectorAll(".error");
                    if (errorClass.length > 0) { // nếu có lỗi thì focus vào phần tử đầu tiên
                        let count = 1;
                        let htmlMessage = Array.from(errorClass).map((item) => {
                            return `${count++}. ${item.getAttribute("data-error")}`;
                        }).join('<br/>');
                        await self.$refs.popup.show({
                            message: htmlMessage,
                            icon: Enum.ICON.ERROR,
                            hideButton: 'true',
                        }).then(() => {
                            self.inputFocus(true);
                        });
                        return false;
                    } else {
                        return true;
                    }
                });
                if (validateResult) {
                    Object.keys(self.tableManger).forEach((key) => {
                        // xóa các trường là null hoặc ""
                        if (self.tableManger[key] == null || self.tableManger[key] === "") {
                            delete self.tableManger[key];
                        }
                    });
                    let result = true;
                    switch (self.formMode) {
                        case Enum.FORM_MODE.ADD: // nếu action form là add thì thực hiện insert
                            result = await self.insertInventoryItem();
                            break;
                        case Enum.FORM_MODE.EDIT: // nếu action form là edit thì thực hiện update
                            result = await self.updateInventoryItem();
                            break;
                        case Enum.FORM_MODE.DUPLICATE: // nếu action form là duplicate thì thực hiện duplicate 
                            result = await self.insertInventoryItem();
                            break;
                    }
                    if (result) { // nếu insert thành công thì xử lý action form
                        switch (action) {
                            case Enum.ACTION.SAVE_AND_CLOSE: // nếu nhấn cất
                                self.closeFormHandle(true);
                                break;
                            case Enum.ACTION.SAVE_AND_ADD: // nếu nhấn cất và thêm
                                self.formMode = Enum.FORM_MODE.NULL;
                                self.formMode = Enum.FORM_MODE.ADD;
                                break;
                            default:
                                break;
                        }
                    }
                }
            } catch (error) {
                if (error.response) {
                    let { status, data } = error.response;
                    if (status == Enum.MISA_CODE.VALIDATE) {
                        let htmlMessage = Object.values(data.moreInfo).map((item) => {
                            return `${item}`;
                        });
                        await self.$refs.popup.showError(htmlMessage);
                    }
                } else {
                    self.$refs.popup.showError(self.$t("notice_message.unknown_error"));
                }
            }
        },
        /**
        * @description: Hàm này dùng để focus vào trường input đầu tiên hoặc là trường lỗi đầu tiên
        * @param: {boolean} isFocusError: có focus vào trường lỗi đầu tiên hay không
        * Author: hvanh 5/3/2023
        */
        inputFocus(isFocusError = false) {
            try {
                let self = this;
                if (isFocusError) {
                    const errorClass = self.$el.querySelector(".error");
                    if (errorClass) {
                        if (errorClass.tagName === "INPUT") {
                            errorClass.focus();
                        }
                        if (errorClass.tagName === "DIV") {
                            errorClass.querySelector("input").focus();
                        }
                    }
                } else {
                    const focusFirst = self.$el.querySelector(".focus").querySelector("input");
                    if (focusFirst) {
                        focusFirst.focus();
                    }
                }
            } catch (error) {
                // console.log(error);
            }
        }
    },
    beforeUnmount() {
        this.formMode = Enum.FORM_MODE.NULL // reset lại formMode
        this.attemptSubmit = false; // reset lại attemptSubmit
        this.$emit("update:modelValue", false); // reset lại modelValue
    },
};
</script>

<style lang="scss" scoped>
.v-dialog__header {
    padding-bottom: 32px;
}

.e-header {
    align-items: center;

    &__title {
        font-size: 24px;
        font-weight: 700;
    }
}

.e-body {
    &__gender {
        height: 32px;
        align-items: center;
    }
}

.v-max-900 {
    &.wide {
        margin: 20px 0;
        max-width: 700px;
        min-width: 400px;
    }
}
</style>