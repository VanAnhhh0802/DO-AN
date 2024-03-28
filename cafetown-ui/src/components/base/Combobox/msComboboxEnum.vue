<template>
  <div class="v-combobox">
    <div class="v-combobox__body" ref="combobox">
      <div
        class="v-combobox__selected"
        :class="[{ error: error }, { 'v-combobox__focus': isFocus }]"
        :tooltip="showError"
        :error="error"
      >
        <!-- Combobox thường thì chỉ cần input để gõ text -->
        <input
          ref="input"
          type="text"
          :value="textInput"
          :placeholder="placeholder"
          @input="handleInput"
          @blur="handleBlurInput"
          autocomplete="off"
          spellcheck="false"
          @focus="handleInputFocus"
          :readonly="selectBox"
          disabled
        />
        <!-- Thêm button add nhanh vào combobox -->
        <!-- Button show list -->
        <button
          class="button combobox__button"
          @click="handleShowListData"
          @keydown="selectItemUpDown"
          tabindex="-1"
        >
        <i class="icon icon--caretdown"
        :class="{'icon--caretdownRotate' : isShowListData}"
      ></i>
        </button>
        <div
          class="combobox__data"
          v-show="isShowListData"
          ref="combobox__data"
        >
          <a
            class="combobox__item"
            v-for="(item, index) in getDataEnum"
            :key="index"
          >
            <span>{{ item }}</span>
          </a>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
/* eslint-disable */
import Enum from "@/utils/enum";
export default {
  name: "msComboboxEnum",
  props: {
    data: {
      type: String,
    },
  },
  data() {
    return {
      isShowListData: false,
    };
  },
  computed: {
    getDataEnum() {
      const me = this;
      let arr = [];
      for (let key in Enum.DATE) {
        for (let text in Enum.DATETEXT) {
          if (key === text) {
            arr.push(Enum.DATETEXT[text]);
          }
        }
      }
      console.log("getDataEnum", arr);
      return arr;
    },
  },
  watch: {},
  methods: {
    handleShowListData() {
      const me = this;
      me.isShowListData = !me.isShowListData;
    },
  },
  mounted() {},
  unmounted() {},
  updated() {},
  /**
   * @description: Hook khởi tạo để gán giá trị vào combo box
   * Author: hvanh 25/09/2022
   */
  created() {
    this.getDataEnum;
  },
};
</script>
<style lang="scss" scoped>
@import "./combobox.css";
</style>
    