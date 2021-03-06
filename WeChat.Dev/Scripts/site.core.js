﻿(function (host, factory) {
    host.utils = factory(host);
}(window, function () {

    /**
     * @description 跳转链接
     * @param {string} url 链接
     */
    function jumpLink(url) {
        if (url) {
            weui.loading('加载中')
            if (url === 'BACK') {
                window.history.back();
            } else {
                window.location.href = url;
            }
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
        var types = ['.xls', '.xlsx', '.doc', '.docx', '.ppt', '.pptx',];
        try {
            //var _type = types.find(function (_type) {
            //    return _type == type;
            //})
            //flag = !!_type;
            for (var i = 0; i < types.length; i++) {
                if (types[i] === type) {
                    flag = true;
                    break;
                }
            }
        } catch (e) {
            console.error(e);
        }
        return flag;
    }

    //强制ios 缓存页面刷新
    function forceReload(source) {
        if (source && source.event.persisted && os().ios) {
            window.location.reload(true)
        }
    }

    /**
    * @description 兼容性扩展 获取data-*
    * @param {DOM} el
    * @param {string|Array} keys
    */
    function dateset(el, keys, serialize) {
        var data = {};
        if (typeof keys === 'string') {
            return getDataSetData(el, keys, serialize);
        }
        if (typeof keys === 'object' && Array.isArray(keys)) {
            keys.forEach(function (key) {
                data[key] = getDataSetData(el, key, serialize);
            })
            return data;
        }
    }

    function getDataSetData(el, key, serialize) {
        var datastr = undefined;
        try {
            if (typeof el.dataset !== 'undefined') {
                datastr = el.dataset[key];
            } else {
                datastr = el.getAttribute("data-" + key);
            }
            if (serialize && datastr) {
                datastr = JSON.parse(datastr);
            }
        } catch (e) {
            console.error(e)
        }
        return datastr;
    }

    /**
     * @description 文件预览
     * @param {object (key-value)} data
     */
    function preview(data) {
        if (!data || !data.doctype || !data.href) {
            return
        }
        if (data.isphoto) {
            return
        }
        var target = null;
        if (os().android) {
            if (isOfficeFile(data.doctype)) {
                target = "https://view.officeapps.live.com/op/view.aspx?src=" + encodeURIComponent(data.href);
                jumpLink(target);
            } else if (data.doctype.toLowerCase() == '.pdf') {
                //暂时未模拟
                var url = "http://ebs.highzap.com/Scripts/plugins/pdfjs/web/viewer.html?file=http://ebs.highzap.com/TempUpFiles/1514541197.pdf";
                jumpLink(url);
            }
        }
        else {//ios
            //直接用浏览器打开
            jumpLink(data.href);
        }
    }

    return {
        os: os,
        on: on,
        dateset: dateset,
        preview: preview,
        jumpLink: jumpLink,
        setTitle: setTitle,
        renderTimego: renderTimego,
        office: isOfficeFile,
        forceReload: forceReload,
        closest: closest
    }
}));

!function () {
    initFastClick();

    /**
     * @description 解决click 300ms延时
     */
    function initFastClick() {
        var attachFastClick = Origami.fastclick;
        attachFastClick(document.body);
    }

    //创建一个axios实例，并为实例添加拦截器代码
    var axiosInstance = axios.create();
    axiosInstance.interceptors.request.use(function (config) {
        // Do something before request is sent
        console.log('开始请求')
        console.log(`请求地址: ${config.url}`)
        return config
    }, function (error) {
        // Do something with request error
        console.log('请求失败')
        return Promise.reject(error)
    })
    axiosInstance.interceptors.response.use(function (config) {
        // Do something before request is sent
        console.log('接收响应')
        return config
    }, function (error) {
        // Do something with request error
        console.log('响应出错')
        return Promise.reject(error)
    });
    window.http = axiosInstance;
}();
