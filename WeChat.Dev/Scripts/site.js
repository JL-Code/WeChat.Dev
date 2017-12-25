(function () {

    try {
        var bodyData = document.body.dataset;
        var identity = new IdentityHandler(bodyData.appcode, bodyData.website);
        window.identityInstance = identity;
        console.log(identity)
        //identity.authorize();

        bindEvents();
    } catch (e) {
        console.error(e);
        weui.alert(e.message);
    }

    /**
     * @description 事件绑定
     */
    function bindEvents() {
        var links = document.querySelectorAll('a');
        links.forEach(function (link) {
            var flag = link.getAttribute('mask')
            if (flag === "true")
                link.addEventListener('click', function (e) {
                    e.preventDefault()
                    weui.loading('加载中')
                    setTimeout(function () {
                        window.location.href = e.target.href
                    }, 50)
                })
        })
    }

    //绑定事件
    function bind(element, event, callback, useCapture) {
        if (element.addEventListener) {
            element.addEventListener(event, callback, useCapture);
        } else {
            // IE8 fallback
            element.attachEvent('on' + event, function (event) {
                // `event` and `event.target` are not provided in IE8
                event = event || window.event;
                event.target = event.target || event.srcElement;
                callback(event);
            });
        }
    }

    //解除绑定事件
    function unbind(element, event, callback, useCapture) {
        if (element.removeEventListener) {
            element.removeEventListener(event, callback, useCapture);
        } else {
            // IE8 fallback
            element.detachEvent('on' + event, callback);
        }
    }
})();