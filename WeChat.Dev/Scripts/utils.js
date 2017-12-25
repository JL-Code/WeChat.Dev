(function (host, factory) {
    host.utils = factory(host);
}(window, function () {

    /**
     * @description 跳转链接
     * @param {string} url 链接
     */
    function jumpLink(url) {
        if (url) {
            weui.loading('加载中')
            window.location.href = url;
        }
    }

    /**
     * @description DOM元素绑定事件
     * @param {DOM | selector} context
     * @param {string} type 事件类型
     * @param {string} selector DOM选择器 eg: .className #id
     * @param {Function} listener 监听函数
       @param {Object} options 一个指定有关 listener 属性的可选参数对象
     */
    function on(context, type, selector, listener, options) {
        if (typeof context === 'string') {
            context = document.querySelectorAll(context);
        }
        document.body.addEventListener(type, function (e) {

        });
    }

    function off() { }

    /**
     * @description 获取当前浏览器的用户代理
     */
    function os() {
        var u = window.navigator.userAgent;
        var android = u.indexOf('Android') > -1 || u.indexOf('Linux') > -1; //安卓
        var ios = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
        return {
            android: android,
            ios: ios
        }
    }

    return {
        jumpLink: jumpLink
    }
}))