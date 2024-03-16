  

$(function () {
    //获取签名等信息
    initSign();
});

function initSign() {
    var urls = window.location.href.split("#")[0]; 
    $.ajax({
        type: "POST",
        url: "/Wechat/WechatOAuth/JsdkInit",
        cache: false,
        data: { 
            "url": urls,
        },
        dataType: "json",
        success: onSignSuccess,
        error: onSignError
    });
}

function onSignSuccess(data) {
    if (data.statusCode != "200") {
        return;
    }
    var appId = data.appId;  //appId
    var nonceStr = data.nonce;  //随机字符串
    var signature = data.signature;  //签名字符串
    var timestamp = data.timestamp;  //时间戳
    var domain = data.domain;
    wx.config({
        debug: false, // 开启调试模式
        appId: appId, // 必填，公众号的唯一标识
        timestamp: timestamp, // 必填，生成签名的时间戳
        nonceStr: nonceStr, // 必填，生成签名的随机串
        signature: signature,// 必填，签名，
        jsApiList: ['hideMenuItems', 'onMenuShareTimeline', 'onMenuShareAppMessage'] // 必填(分享给朋友，分享到朋友圈)
    });
      
    wx.ready(function () {
        //自定义分享-分享到朋友圈
        wx.onMenuShareTimeline({
            title: titles, // 分享标题
            link:domain + shareUrl, // 分享链接
            desc: descs,
            imgUrl: domain + imgUrls, // 分享图标
            success: function () {
                // 用户确认分享后执行的回调函数
            }, cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });

        //自定义分享-分享给朋友
        wx.onMenuShareAppMessage({
            title: titles, // 分享标题
            desc: descs, // 分享描述
            link: domain + shareUrl, // 分享链接
            imgUrl: domain + imgUrls, // 分享图标
            //type: '', // 分享类型,music、video或link，不填默认为link
            //dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
            success: function () {
                // 用户确认分享后执行的回调函数
            }, cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });

        //隐藏右上角部分菜单
        wx.hideMenuItems({
            menuList: [
		        "menuItem:share:qq",  //分享到QQ
		        "menuItem:share:weiboApp",  //分享到Weibo
		        "menuItem:favorite",  //收藏
		        "menuItem:share:facebook",  //分享到FB:
		        "menuItem:share:QZone",  //分享到 QQ 空间
		        "menuItem:delete",  //删除
		        "menuItem:copyUrl",  //复制链接
		        "menuItem:openWithSafari", //在Safari中打开
		        "menuItem:share:email",  //邮件
		        "menuItem:readMode",  //阅读模式:
		        "menuItem:originPage",  //原网页
		        "menuItem:exposeArticle"  //举报
            ], success: function (res) {
                //alert('已隐藏“阅读模式”，“分享到朋友圈”，“复制链接”等按钮');  
            }, fail: function (res) {
                //alert(JSON.stringify(res));  
            }
        });
    });
}

//加载签名信息失败
function onSignError(XMLHttpRequest, textStatus, errorThrown) {
    // 	alert(XMLHttpRequest.status);
    //     alert(XMLHttpRequest.readyState);
    //     alert(textStatus);
}

