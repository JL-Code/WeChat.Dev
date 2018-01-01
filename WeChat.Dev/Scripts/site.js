(function () {

    try {
        //var bodyData = document.body.dataset;
        //var identity = new IdentityHandler(bodyData.appcode, bodyData.website);
        //window.identityInstance = identity;
        //console.log(identity)
        ////identity.authorize();

        bindEvents();
    } catch (e) {
        console.error(e);
        weui.alert(e.message);
    }

    /**
     * @description 事件绑定
     */
    function bindEvents() {
        //utils.on('.list', 'click', 'a', function (e) {
        //    alert(1212);
        //})
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