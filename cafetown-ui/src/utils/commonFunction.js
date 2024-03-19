import {Buffer} from 'buffer';

class commonFn{
    /**
     * Hàm đăng nhâp
     */
    // async login(){
    //     const response = await me.$api.authen.login(me.username, me.password);
    //     if (response && response.status == Enum.MISA_CODE.SUCCESS) {  
    //         this.$store.commit('setPermission', response.data);              
    //         this.$router.push('/tong-quan')
    //     } else {
    //         me.messageError = me.$t('login.error_message.userAndPassword');
    //         me.showDialog = true;
    //     }
    // }

    /**
     * Convert chuỗi utf8 thành chuỗi base64
     * @param {string} obj 
     * @returns 
     */
    convertToBase64(obj){
        if(typeof obj !== 'string'){
            obj = JSON.stringify(obj);
        }
        let buffer = Buffer.from(obj);
        let base64 = buffer.toString('base64');
        return base64;
    }
    
    /**
     * Convert base64 thành chuỗi utf8
     * @param {base64} base64String 
     * @returns 
     */
    convertFromBase64(base64String){
        let buffer = Buffer.from(base64String,'base64');
        let object = buffer.toString('utf8');
        return object;
    }
}

export default new commonFn();