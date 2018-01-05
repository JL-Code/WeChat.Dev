(function () {

    try {
        var identity = new IdentityHandler(dateset(document.body, "appcode"), dateset(document.body, "website"));
        window.identityInstance = identity;
        //identity.authorize();
    } catch (e) {
        console.error(e);
        weui.alert("出现错误：" + e.message);
    }

})();

function showAction() {
    var labelHtml = '<span style= "vertical-align: middle" >我的发起</span>' +
        '<span class="weui-badge" style="margin-left: 5px;">99+</span>';
    weui.actionSheet([{
        label: '我的发起',
        onClick: function () {
            utils.jumpLink("/home/myapplication");
        }
    },
    {
        label: '我的办理',
        onClick: function () {
            utils.jumpLink("/home/mytransaction");
        }
    }
    ], [{
        label: '取消',
        onClick: function () {
            console.log('取消');
        }
    }]);
}

/**
 * @description 链接跳转方法
 */
function bindJumpLink() {
    var links = document.querySelectorAll('a');
    links.forEach(function (link) {
        var flag = link.getAttribute('mask')
        if (flag === "true")
            link.addEventListener('click', function (e) {
                e.preventDefault()
                utils.jumpLink(this.href);
            })
    })
}

/**
 * @description 渲染时间
 */
function renderTimeGo() {
    if (typeof timeago === 'function') {
        var timeagoInstance = timeago();
        timeagoInstance.render(document.querySelectorAll('.time_need_to_be_rendered'), 'zh_CN');
    }
}

/**
 * @description 绑定事件
 * @param {DOM} element
 * @param {string} event
 * @param {function} callback
 * @param {bool} useCapture
 */
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

/**
 * @description 解除绑定事件
 * @param {DOM} element
 * @param {string} event
 * @param {function} callback
 * @param {bool} useCapture
 */
function unbind(element, event, callback, useCapture) {
    if (element.removeEventListener) {
        element.removeEventListener(event, callback, useCapture);
    } else {
        // IE8 fallback
        element.detachEvent('on' + event, callback);
    }
}

