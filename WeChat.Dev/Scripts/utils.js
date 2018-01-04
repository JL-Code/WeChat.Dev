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
            e.preventDefault();
            var eTarget = e.target;
            var selectorEl = context[0].querySelector(selector);
            var clickedListItem = closest(eTarget, function (el) {
                return el.querySelector(selector);
            });
            if (eTarget === selectorEl || clickedListItem) {
                listener(e);
            }
        });
    }

    //递归查找最近父元素
    function closest(el, fn) {
        return el && (fn(el) ? el : closest(el.parentNode, fn));
    };



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

    /**
     * @description 设置文档标题
     * @param {string} name
     * @param {int} num
     */
    function setTitle(name, num) {
        var titText = num === undefined ? name : (name + "( " + num + " )");
        var title = document.getElementsByTagName('title');

        function iframeLoad() {
            setTimeout(function () {
                iframeEl.removeEventListener('load', iframeLoad);
                iframeEl.remove();
            }, 0)
        }

        if (os().ios) {
            var bodyEl = document.body;
            var iframeEl = document.createElement('iframe');
            document.title = titText;
            iframeEl.src = '/Content/images/touming.png';
            iframeEl.onload = iframeLoad;
            bodyEl.appendChild(iframeEl);
            return
        }
        title[0].innerText = titText;
    };

    /**
     *@description 渲染timego时间
     * @param {string} selector
     */
    function renderTimego(selector) {
        if (typeof timeago === 'function') {
            var timeagoInstance = timeago();
            var times = document.querySelectorAll(selector);
            var tempTime;
            var now = Date.now();
            try {
                times.forEach(function (time) {
                    tempTime = time.getAttribute('datetime');
                    if (tempTime) {
                        var temp = new Date(tempTime);
                        if ((now - temp.getTime()) > 86400000) {
                            time.innerHTML = tempTime.substring(0, 16);
                        }
                        else {
                            time.innerHTML = timeagoInstance.format(tempTime, 'zh_CN');
                        }
                    }
                })
            } catch (e) {
                console.error(e)
            }
        }
    }

    function isOfficeFile(type) {
        var flag = false;
        var types = ['.xls', '.xlsx', '.word', '.wordx', '.ppt', '.pptx',];
        try {
            var _type = types.find(function (_type) {
                return _type == type;
            })
            flag = !!_type;
        } catch (e) {
            console.error(e);
        }
        return flag;
    }

    return {
        os: os,
        on: on,
        jumpLink: jumpLink,
        setTitle: setTitle,
        renderTimego: renderTimego,
        office: isOfficeFile
    }
}))