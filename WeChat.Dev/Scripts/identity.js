(function (host, factory) {

    'use strict';
    host.IdentityHandler = factory(utils);

}(window, function (utils) {

    'use strict';

    var _key = "_identity_";
    var _authorizePath = "/wechat/authorize";

    /**
     * @description 构造函数
     * @param {string} appcode 应用编码
     * @param {string} website 网站域名
     */
    function IdentityHandler(appcode, website) {
        if (!appcode)
            throw new Error("appcode undefined");
        if (!website)
            throw new Error("website undefined");

        _key = appcode + _key;
        _authorizePath = website + _authorizePath;

        this.appcode = appcode;
        this.website = website;

    }

    //授权
    function _authorize() {
        var loading;
        try {
            loading = weui.loading();
            if (!this.verifyToken(_key)) {
                utils.jumpLink(_authorizePath);
            } else {
                loading.hide();
            }
        } catch (e) {
            console.error(e)
            loading && loading.hide();
            weui.alert(e.message, { title: '警告' });
        }
    }

    //认证
    function _authenticate() {
        throw new Error("未实现的方法");
        //var loading;
        //try {
        //    loading = weui.loading();
        //    if (!this.verifyToken(_key)) {
        //        window.location.href = _authorizePath;
        //    }
        //} catch (e) {
        //    console.error(e)
        //    loading && loading.hide();
        //    weui.alert(e.message, { title: '警告' });
        //}
    }

    function _verifyToken(key) {

        var flag = false;
        var nowTimeStamp = Date.now();
        try {
            var token = this.getToken(key);
            if (token && token.expires_in) {
                if (token.expires_in > nowTimeStamp) {
                    flag = true;
                } else {
                    //身份过期 通过刷新令牌重新获取
                }
            }
        } catch (e) {
            console.error(e)
        }
        return flag;
    }

    function _saveToken(key, token) {
        var tokenStr = token;
        if (typeof token === 'object') {
            tokenStr = JSON.stringify(token);
        }
        localStorage.setItem(key, tokenStr);
    }

    function _getToken(key) {
        try {
            var tokenStr = localStorage.getItem(key);
            return JSON.parse(tokenStr);
        } catch (e) {
            return {};
        }
    }

    function _removeToken(key) {
        localStorage.removeItem(key);
    }

    IdentityHandler.prototype = {
        constructor: IdentityHandler,
        authorize: _authorize,
        authenticate: _authenticate,
        verifyToken: _verifyToken,
        saveToken: _saveToken,
        getToken: _getToken,
        removeToken: _removeToken,
        login: function (acc, pwd, workUserId, returnUrl) {
            var _this = this;
            var loading = weui.loading('登录中');
            axios
                .post('/account/login', {
                    account: acc,
                    password: pwd,
                    workUserId: workUserId
                })
                .then(function (response) {
                    var data = response.data;
                    if (data.errcode !== 0) {
                        weui.alert(data.errmsg);
                    } else {
                        this.saveToken(_key, data.token);
                        utils.jumpLink(returnUrl)
                    }
                })
                .catch(function (err) {
                    throw new Error(err)
                });
        },
        wxLogin: function (nonce, returnUrl) {
            var _this = this;
            var loading = weui.loading('登录中');
            axios
                .post('/account/worklogin', {
                    nonce: nonce,
                })
                .then(function (response) {
                    var data = response.data;
                    if (data.errcode !== 0) {
                        loading.hide();
                        weui.alert(data.errmsg, { buttons: [] });
                    } else {
                        _this.saveToken(_key, data.token)
                        utils.jumpLink(returnUrl)
                    }
                })
                .catch(function (err) {
                    throw new Error(err)
                });
        }
    }

    return IdentityHandler;
}));


