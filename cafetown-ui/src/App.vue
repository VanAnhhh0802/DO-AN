
<template>
  <div class="main">
    <div class="menu-container">
      <TheSidebar :collapsed="collapsed" @toggleSidebar="toggleSidebar" />
    </div>
    <div class="content-area">
      <TheHeader @toggleSidebar="toggleSidebar" :collapsed="collapsed" />
      <div class="content">
        <router-view />
      </div>
    </div>
    <v-quick-key></v-quick-key>
  </div>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      collapsed: false
    };
  },
  methods: {
    /**
     * Đóng mở sidebar
     * Author: hvanh 10/09/2022
     */
    toggleSidebar() {
      this.collapsed = !this.collapsed;
    }
  }
}
</script>
