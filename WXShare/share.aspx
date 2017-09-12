<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="share.aspx.cs" Inherits="WXShare.share" %><!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>微信分享样例</title>	
</head>
<body>  
	<img src="http://test.yulegameyun.cn/res/Card/1472524586000/1.jpg" alt="test">
</body>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script>
    wx.config({
        debug: false,
        appId: '<%=AppId%>',
        timestamp: <%=Timestamp%>,
        nonceStr: '<%=NonceStr%>',
        signature: '<%=Signature%>',
        jsApiList: [
            // 所有要调用的 API 都要加到这个列表中
            'checkJsApi',           
            'onMenuShareTimeline',
            'onMenuShareAppMessage',
			'onMenuShareQQ',
			'onMenuShareWeibo',
			'onMenuShareQZone'
          ]
    });
    

     
 wx.ready(function () {
	 
	 //分享给朋友
        wx.onMenuShareAppMessage({
    title: '这是一个标题这是一个标题这是一个标题', // 分享标题
    desc: '这是一段描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述', // 分享描述
    //link: '', // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
    imgUrl: 'http://test.yulegameyun.cn/res/Card/1477897929000/2.jpg', // 分享图标
    type: '', // 分享类型,music、video或link，不填默认为link
    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
    success: function () { 
        // 用户确认分享后执行的回调函数
         alert('已分享');

    },
    cancel: function () { 
        // 用户取消分享后执行的回调函数
         alert('已取消');

    }
	});
	//分享到朋友圈
	  wx.onMenuShareTimeline({
    title: '这是一个标题这是一个标题这是一个标题', // 分享标题
    desc: '这是一段描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述', // 分享描述
    //link: '', // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
    imgUrl: 'http://test.yulegameyun.cn/res/Card/1477897929000/2.jpg', // 分享图标
    type: '', // 分享类型,music、video或link，不填默认为link
    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
    success: function () { 
        // 用户确认分享后执行的回调函数
         alert('已分享');

    },
    cancel: function () { 
        // 用户取消分享后执行的回调函数
         alert('已取消');

    }
});

//分享到QQ
  wx.onMenuShareQQ({
    title: '这是一个标题这是一个标题这是一个标题', // 分享标题
    desc: '这是一段描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述', // 分享描述
    //link: '', // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
    imgUrl: 'http://test.yulegameyun.cn/res/Card/1477897929000/2.jpg', // 分享图标
    type: '', // 分享类型,music、video或link，不填默认为link
    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
    success: function () { 
        // 用户确认分享后执行的回调函数
         alert('已分享');

    },
    cancel: function () { 
        // 用户取消分享后执行的回调函数
         alert('已取消');

    }
});
//分享到腾讯微博
  wx.onMenuShareWeibo({
    title: '这是一个标题这是一个标题这是一个标题', // 分享标题
    desc: '这是一段描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述', // 分享描述
    //link: '', // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
    imgUrl: 'http://test.yulegameyun.cn/res/Card/1477897929000/2.jpg', // 分享图标
    type: '', // 分享类型,music、video或link，不填默认为link
    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
    success: function () { 
        // 用户确认分享后执行的回调函数
         alert('已分享');

    },
    cancel: function () { 
        // 用户取消分享后执行的回调函数
         alert('已取消');

    }
});
//分享到QQ空间
  wx.onMenuShareQZone({
    title: '这是一个标题这是一个标题这是一个标题', // 分享标题
    desc: '这是一段描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述', // 分享描述
    //link: '', // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
    imgUrl: 'http://test.yulegameyun.cn/res/Card/1477897929000/2.jpg', // 分享图标
    type: '', // 分享类型,music、video或link，不填默认为link
    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
    success: function () { 
        // 用户确认分享后执行的回调函数
         alert('已分享');

    },
    cancel: function () { 
        // 用户取消分享后执行的回调函数
         alert('已取消');

    }
});
       
  });
    
</script>
</html>