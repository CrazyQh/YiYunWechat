$(function () {

    //$("#left li:first-child").addClass("active");
    var e;
    //////无规格商品直接添加
    $(".add").click(function () {
        //alert("购物车 - 加001");
        e = $(this).prev(); //获取下添加按钮的上一个元素
        var n = $(this).prev().text();//取出来添加按钮前的数量
        var num = parseFloat(n); //转换浮点型
        if (n == 0) { num = 1 }
        else { num = num+1  }
        $(this).prev().text(num);  //更新菜品列表数量

        var parent = $(this).parent();//获取父级元素
        var m = parent.parent().children("h4").text().trim();        //获取规格弹窗当前商品名称
        var taste = "默认";                        //获取规格弹窗当前所选规格
        var acount = 1;             //取规格弹窗的数量
        var dataIconN = $(this).parent().parent().children("h4").attr("data-icon");//获取data-icon 
        //var gousum = parseFloat($("span[data-icon=" + dataIconN + "]").parent().parent().find(">div>.accountPrice").text());
        var sum = parseFloat(parent.prev().children("b:nth-child(2)").text()) * acount ; //获取规格弹窗单价*数量+购物车金额
        var price = parseFloat(parent.prev().children("b:nth-child(2)").text())       //获取规格弹窗单价
        var GoodsUnit = parent.prev().children("b:nth-child(4)").text();       //获取规格
        var goodscode = $(this).parent().parent().children("h4").attr("id");      //获取商品编码
        var LogoUrl = $(this).parent().parent().parent().children().children("img").attr("src");      //获取商品图片路径

        //AJAX开始##############################################################################
        var goods = JSON.stringify({ ShopCode: $(".shopcode").html(), ShopName: $(".shopname").html(), GoodsID: dataIconN, GoodsCode: goodscode, GoodsFullName: m, GoodsUnit: GoodsUnit, LogoUrl: LogoUrl, Qty: acount, SJ: price, Amount: sum, Remark: taste })
        $.ajax({
            url: "/OrderFood/ShopAddFood",    //方法
            type: 'post',
            async: false,
            data: { goods: goods, openid: $(".openid").html(), nickname: $(".nickname").html(), VillCode: $(".villcode").html() },
            success: function (result) {
                $.hideLoading();
                if (result.statuscode == "200") {

                    var a = $("#totalpriceshow").html();  //获取当前所选总价
                    $("#totalpriceshow").html((a * 1 + price * 1).toFixed(2));//底部计算当前所选总价
                    var nm = $("#totalcountshow").html(); //底部获取数量
                    $("#totalcountshow").html(nm * 1 + 1);    //底部更新数量
                    e.css("display", "inline-block");
                    e.prev().css("display", "inline-block")
                    jss();//改变按钮样式

                    //判断购物车里是否有商品，是否有相同规格的商品
                    if ($(".list-content ul li").length <= 0) {
                        var addtr = '<li class="food">';
                        addtr += '<div><span class="accountName" data-icon="' + dataIconN + '">' + m + '</span><span class="taste">' + taste + '</span></div>';
                        addtr += '<div><span>￥</span><span class="accountPrice">' + sum + '</span></div>';
                        addtr += '<div class="btn">';
                        addtr += '<button class="ms2" style="display: inline-block;"></button>';
                        addtr += '<i class="li_acount">' + acount + '</i>';
                        addtr += '<button class="ad2"></button>';
                        addtr += '<i class="price" style="display: none;">' + price + '</i>';
                        addtr += '<i class="goodscode" style="display: none;">' + goodscode + '</i>';
                        addtr += '</div>';
                        addtr += '</li>';
                        $(".list-content ul").append(addtr);
                        return;
                    } else {
                        $(".list-content ul li").each(function () {
                            if ($(this).find("span.accountName").html() == m && $(this).find(".taste").html() == taste) {
                                //var sum = parseFloat($(this).find(".accountPrice").html());
                                //var sum = parseFloat($(".food div .accountPrice").text());
                                var count = parseInt($(this).find(".li_acount").html());
                                count += parseInt(acount);
                                $(this).find(".accountPrice").html((sum * count).toFixed(2)); //对商品的金额进行重新赋值
                                $(this).find(".li_acount").html(count); //对商品的数量进行重新赋值
                                flag = true;
                                return false;
                            } else {
                                flag = false;
                            }
                        })
                    }
                    //如果为默认值也就是说里面没有此商品，所以添加此商品。
                    if (flag == false) {
                        var addtr = '<li class="food">';
                        addtr += '<div><span class="accountName" data-icon="' + dataIconN + '">' + m + '</span><span class="taste">' + taste + '</span></div>';
                        addtr += '<div><span>￥</span><span class="accountPrice">' + sum + '</span></div>';
                        addtr += '<div class="btn">';
                        addtr += '<button class="ms2" style="display: inline-block;"></button><i class="li_acount">' + acount + '</i><button class="ad2"></button><i class="price" style="display: none;">' + price + '</i><i class="goodscode" style="display: none;">' + goodscode + '</i><i class="goodsunit" style="display: none;">' + GoodsUnit + '</i><i class="logourl" style="display: none;">' + LogoUrl + '</i>';
                        addtr += '</div>';
                        addtr += '</li>';
                        $(".list-content ul").append(addtr);
                    }

                }
                if (result.statusCode == "300") {
                    $.toast(result.message, "警告");
                }
            }
        });
        //AJAX结束##############################################################################

    }); 
	//////商品点击选规格
    $('.menu-txt .btn').on('click', '.xuan', function () {
       //$(".xuan").click(function () {
        $.ajax({                    //取规格数据
            type: "GET",
            contentType: "application/json",
            url: "/OrderFood/GetGoodsSpec",    //方法
            data: { id: $(this).attr('id')},  //取选中行的ID作参数
            dataType: "json",   
            success: function (data) {
                
                $('.subChose').empty();   //清空里面的所有内容
                var html = '<dt>规格</dt> ';  //组装前加上表头
                $.each(data.list, function (key, val) {
                    if (key== 0)  //如果第一条
                    {
                        $(".subName dd p:nth-child(2) span:nth-child(2)").text(val.SJ);  // 价格赋值
                        $(".subName dd p:nth-child(2) span:nth-child(3)").text('元/' +val.GOODSUNIT);  // 价格赋值
                        $(".subName dd p:nth-child(3) span:nth-child(2)").text(val.GOODSSPEC);  // 规格赋值
                        html += ' <dd class="m-active">' + val.GOODSSPEC + ' </dd><dt id="' + val.GOODSSPEC + '" style="display:none">' + val.SJ + '</dt>'; //默认选中状态
                    }
                    else
                    {  //style="cursor :pointer;"
                        html += ' <dd  >' + val.GOODSSPEC + '</dd> <dt id="' + val.GOODSSPEC + '" style="display:none">' + val.SJ + '</dt>';  //其它没有选中
                    }
                    
                });
                $('.subChose').html(html);   
            }
        });

        $(".subFly").show();  //显示选择规格DIV
        var n = $(this).prev().text();//取出来添加按钮前的数量
        var num = parseFloat(n); //转换浮点型
        if(n==0){num =1}
        $(".ad").prev().text(num);  //如果NUM为1则更新下数量
        e = $(this).prev(); //获取下添加按钮的上一个元素
		
        var parent = $(this).parent();//获取父级元素
        var name = parent.parent().children("h4").text();// 获取父级的父级的子级H4元素的值
        var goodscode = parent.parent().children("h4").attr("id"); //获取商品编码
        var price = parseFloat(parent.prev().children("b:nth-child(2)").text());//父级的上一个元素第二个子元素价格
        var src = $(this).parent().parent().prev().children()[0].src;  //获取图片路径
        var goodsunit = parent.prev().children("b:nth-child(4)").text();       //获取规格
        
        $(".subName dd p:nth-child(1)").html(name); //把菜名赋值给dd的第一个子标签
        $(".pce").text(price);      //赋值给价格
        $(".goodsut").text(goodsunit);      //赋值给单位
        $(".imgPhoto").attr('src', src);  //赋值给图片路径
        $(this).siblings(".price").text(price);  //更新原先按钮后的价格
        $(".goodsc").text(goodscode); // 更新商品编码
        $(".mydd .price").html(price);   // 赋值给价格
        $(".choseValue").text($(".subChose .m-active").text()); //更新上面的已选规格
        var dataIcon = $(this).parent().parent().children("h4").attr("data-icon");//获取商品ID
        $(".subName dd p:first-child").attr("data-icon",dataIcon);// 添加ID
    });
    //商品列表减
    $(".minus").click(function () {
        //alert("商品列表减002");
        var ispec = $(this).parent().find(">.ispec").text();  //取是否有规格
        if (ispec == '0') {  //没有规格直接减
            e = $(this).next(); //获取下添加按钮的下一个元素（数量）
            var a = parseFloat($(this).siblings(".price").text());  //当前商品单价
            var n = parseInt($(this).next().text()) - 1;  //当前商品数量
            var s = parseFloat($("#totalpriceshow").text());  //总金额
            //////var m = parent.parent().children("h4").text().trim();        //获取规格弹窗当前商品名称
            //////var GoodsUnit = parent.prev().children("b:nth-child(4)").text();       //获取规格
            //////var goodscode = $(this).parent().parent().children("h4").attr("id");      //获取商品编码
            var dataIcon = $(this).parent().parent().children("h4").attr("data-icon");  //获取dataicon
            var goun = $(".food div span:first-child").attr("data-icon");
            
            var fh = $(this);
            //AJAX开始（商品列表减）##############################################################################
            $.ajax({
                url: "/OrderFood/ShopDelFood",    //方法
                type: 'post',
                async: false,
                data: { GoodsID: goun, OpenID: $(".openid").html(), Remark:"默认" },
                success: function (result) {
                    $.hideLoading();
                    if (result.statuscode == "200") {

                        if (n == 0) {
                            $("span[data-icon=" + dataIcon + "]").parent().parent().find(">.btn>.li_acount").text(n);
                            $("span[data-icon=" + dataIcon + "]").parent().parent().find(">div>.accountPrice").text((a * n).toFixed(2));
                            e.css("display", "none");
                            e.prev().css("display", "none");
                            $(".up1").hide();
                            $("span[data-icon=" + dataIcon + "]").parent().parent().remove();
                        }
                        else {
                            //$("span[data-icon=" + dataIcon + "]").parent().find(">.btn>.li_acount").text(n);   //更新菜单列表的数量
                            $("span[data-icon=" + dataIcon + "]").parent().parent().find(">.btn>.li_acount").text(n);
                            $("span[data-icon=" + dataIcon + "]").parent().parent().find(">div>.accountPrice").text((a * n).toFixed(2));
                        }

                        fh.next().text(n);//赋值给商品列表的数量 (AJAX里this需要在外面先声明下)
                        //$(this).next().text(n);//赋值给商品列表的数量 
                        //$(this).parent().prev().children("span:nth-child(2)").text(a * n);  // 更新购物车的价格

                        $("#totalcountshow").text(parseInt($("#totalcountshow").text()) - 1);
                        $("#totalpriceshow").text((s * 1 - a * 1).toFixed(2));
                        if (parseFloat($("#totalcountshow").text()) == 0) {
                            $(".shopcart-list").hide();
                            jss();
                        }
                    }
                    if (result.statusCode == "300") {
                        $.toast(result.message, "警告");
                    }
                }
            });
           //AJAX结束（商品列表减）##############################################################################


        }
        else     //有规格弹出购物车
        {
            $('.shopcart-list,.up1').show();
        }

    });
	
	//规格选择事件
    $('.down').on('click', '.subChose dd', function () {
        //alert("规格选择事件");
        $(this).addClass("m-active").siblings().removeClass("m-active");  //选择规格后移除其他元素样式
        $(".choseValue").text($(".subChose .m-active").text());           //选择规格后设置选中状态
        var sj = $(this).next().text();                                   //取新规格价格
        $(".subName dd p:nth-child(2) span:nth-child(2)").text(sj);       //新规格价格赋值
        
    })
	
	//弹框 - 加
    $(".ad").click(function () {
        //alert("弹框 - 加");
        var n = parseFloat($(this).prev().text())+1;
        if (n == 0) { return; }
        $(this).prev().text(n);
        var danjia = $(this).next().text();   //获取单价
        var a = $("#totalpriceshow").html();  //获取当前所选总价
        var b = (a * 1 + danjia * 1).toFixed(2);
        $("#totalpriceshow").html(b);         //计算当前所选总价
        var nm = $("#totalcountshow").html(); //获取数量

        $("#totalcountshow").html(nm*1+1);
    });
	
    //弹框 - 减
    //$(document).on('click', '.ms', function () {
    $(".ms").click(function () {
        //alert("弹框 - 减");
        var n = $(this).next().text();
        if(n>0){
            var num = parseFloat(n) - 1;
            $(this).next().text(num);//减1

            var danjia = $(this).nextAll(".price").text();//获取单价
            var a = $("#totalpriceshow").html();//获取当前所选总价
            var b = (a*3 - danjia*3)/3;
        	$("#totalpriceshow").html(b);//计算当前所选总价

            var nm = $("#totalcountshow").html();//获取数量
            $("#totalcountshow").html(nm * 1 - 1);
        }

        //如果数量小于或等于0则隐藏减号和数量
        if (num <= 0) {
            $(this).next().css("display", "none");
            $(this).css("display", "none");
            jss();//改变按钮样式
            return
        }
    });
	
	//点击遮罩，隐藏商品详情弹框
    $(".up").click(function(){
        $(".subFly").hide();  
    });
	
	//点击选后加入购物车按钮
    var flag = false;
    $(".down").on('click', '.foot', function () {
        //alert("点击选后加入购物车按钮003");
        //var n = $('.ad').prev().text(); //取规格弹窗的数量
        var n = 1;
        var num = parseFloat(n) + 1;    //num加1
        if (num == 0) { return; }       //等于0 则返回
        $('.ad').prev().text(num);      //设置数量 

        //var danjia = $('.ad').next().text();  //取规格弹窗单价
        var danjia = parseFloat($(".subName dd p:nth-child(2) span:nth-child(2)").text())
        var a = $("#totalpriceshow").html();  //获取当前所选总价
        $("#totalpriceshow").html((a * 1 + danjia * 1).toFixed(2));//底部计算当前所选总价
        var nm = $("#totalcountshow").html(); //底部获取数量
        $("#totalcountshow").html(nm*1+1);    //底部更新数量
        jss();   //改变结算按钮样式
        $(".subFly").hide();  //隐藏规格弹窗
        var aa = e.text();
        var bb = parseFloat(aa) + 1; 
        var ms = e.text(bb);//更新主页面数量
        //var ms = e.text(num - 1);//更新主页面数量
        if (ms!=0){      //判断是否显示减号及数量
            e.css("display","inline-block");
            e.prev().css("display","inline-block")
        }
        var m = $(".subName dd:nth-child(2) p:nth-child(1)").text();        //获取规格弹窗当前商品名称
        var taste = $(".subChose .m-active").text();                        //获取规格弹窗当前所选规格
        var acount = n;             //取规格弹窗的数量
        var sum = (parseFloat($(".subName dd p:nth-child(2) span:nth-child(2)").text()) * parseFloat(acount)).toFixed(2); //获取规格弹窗单价*数量
        var price = parseFloat($(".subName dd p:nth-child(2) span:nth-child(2)").text());        //获取规格弹窗单价
        var dataIconN = $(this).parent().children(".subName").children("dd").children("p:first-child").attr("data-icon");//获取data-icon 
        var goodscode = $(".goodsc").text(); 
        var logourl = $(".imgPhoto").attr("src"); 
        var goodsut = $(".goodsut").text();
        var qty = "1";
        //AJAX开始（商品列表加）##############################################################################
        var goods = JSON.stringify({ ShopCode: $(".shopcode").html(), ShopName: $(".shopname").html(), GoodsID: dataIconN, GoodsCode: goodscode, GoodsFullName: m.trim(), GoodsUnit: goodsut, LogoUrl: logourl, Qty: qty, SJ: price, Amount: price, Remark: taste })
        $.ajax({
            url: "/OrderFood/ShopAddFood",    //方法
            type: 'post',
            async: false,
            data: { goods: goods, openid: $(".openid").html(), nickname: $(".nickname").html(), VillCode: $(".villcode").html() },
            success: function (result) {
                $.hideLoading();
                if (result.statuscode == "200") {
                    //判断购物车里是否有商品，是否有相同规格的商品
                    if ($(".list-content ul li").length <= 0) {
                        var addtr = '<li class="food">';
                        addtr += '<div><span class="accountName" data-icon="' + dataIconN + '">' + m + '</span><span class="taste">' + taste + '</span></div>';
                        addtr += '<div><span>￥</span><span class="accountPrice">' + sum + '</span></div>';
                        addtr += '<div class="btn">';
                        addtr += '<button class="ms2" style="display: inline-block;"></button>';
                        addtr += '<i class="li_acount">' + acount + '</i>';
                        addtr += '<button class="ad2"></button>';
                        addtr += '<i class="price" style="display: none;">' + price + '</i>';
                        addtr += '<i class="goodscode" style="display: none;">' + goodscode + '</i>';
                        addtr += '</div>';
                        addtr += '</li>';
                        $(".list-content ul").append(addtr);
                        return;
                    } else {
                        $(".list-content ul li").each(function () {
                            if ($(this).find("span.accountName").html() == m && $(this).find(".taste").html() == taste) {
                                var count = parseInt($(this).find(".li_acount").html());
                                var sumprice = parseFloat($(this).find(".accountPrice").html());
                                count += parseInt(n);
                                sumprice = (parseFloat(sumprice) * 1 + parseFloat(price) * 1).toFixed(2);
                                $(this).find(".li_acount").html(count); //对商品的数量进行重新赋值
                                $(this).find(".accountPrice").html(sumprice); //对商品的数量进行重新赋值
                                flag = true;
                                return false;
                            } else {
                                flag = false;
                            }
                        })
                    }
                    //如果为默认值也就是说里面没有此商品，所以添加此商品。
                    if (flag == false) {
                        var addtr = '<li class="food">';
                        addtr += '<div><span class="accountName" data-icon="' + dataIconN + '">' + m + '</span><span class="taste">' + taste + '</span></div>';
                        addtr += '<div><span>￥</span><span class="accountPrice">' + sum + '</span></div>';
                        addtr += '<div class="btn">';
                        addtr += '<button class="ms2" style="display: inline-block;"></button><i class="li_acount">' + acount + '</i><button class="ad2"></button><i class="price" style="display: none;">' + price + '</i><i class="goodscode" style="display: none;">' + goodscode + '</i>';
                        addtr += '</div>';
                        addtr += '</li>';
                        $(".list-content ul").append(addtr);
                    }
                }
                if (result.statusCode == "300") {
                    $.toast(result.message, "警告");
                }
            }
        });
        //AJAX结束（商品列表加）##############################################################################

		
    });

	//购物车 - 加
    $('.list-content').on('click', '.ad2', function () {
        //alert("购物车 - 加004");
        var n = parseInt($(this).prev().text()) + 1;  //购物车数量+1
        $(this).prev().text(n);    //当前商品数量+1

        var cid = $(this).parent().siblings().children(".accountName").attr("data-icon");  //获取data-icon 
        var xqty = parseInt($("h4[data-icon=" + cid + "]").parent().find(">.btn>.qty").text())
        $("h4[data-icon=" + cid + "]").parent().find(">.btn>.qty").text(xqty+1);   //更新菜单列表的数量
		//e.text(n);    //赋值给商品列表的数量
        var p = parseFloat($(this).next().text());    //隐藏的价格
        $(this).parent().prev().children("span.accountPrice").text((p * n).toFixed(2));  //计算该商品规格的总价值
        var goodscode = $(this).next().next().text();  //获取商品编码
        var goodsname = $(this).parent().siblings().children(".accountName").text();  //获取商品名称
        var goodsunit = $(this).next().next().next().text();  //获取商品单位
        var logourl = $(this).next().next().next().next().text();  //获取商品图片
        var sqty = "1";
        var remark = $(this).parent().siblings().children(".taste").text();  //获取商品名称
	   //AJAX开始（商品列表加）##############################################################################
        var goods = JSON.stringify({ ShopCode: $(".shopcode").html(), ShopName: $(".shopname").html(), GoodsID: cid, GoodsCode: goodscode, GoodsFullName: goodsname, GoodsUnit: goodsunit, LogoUrl: logourl, Qty: sqty, SJ: p, Amount: p, Remark: remark })
        $.ajax({
            url: "/OrderFood/ShopAddFood",    //方法
            type: 'post',
            async: false,
            data: { goods: goods, openid: $(".openid").html(), nickname: $(".nickname").html(), VillCode: $(".villcode").html() },
            success: function (result) {
                $.hideLoading();
                if (result.statuscode == "200") {
                    $("#totalcountshow").text(parseFloat($("#totalcountshow").text()) + 1);   //总数量＋1
                    $("#totalpriceshow").text((parseFloat($("#totalpriceshow").text()) + p).toFixed(2));   //总价加上该商品价格
                }
                if (result.statusCode == "300") {
                    $.toast(result.message, "警告");
                }
            }
        });
        //AJAX结束（商品列表加）##############################################################################

	});
	
	//购物车 - 减
    $('.list-content').on('click', '.ms2', function () {
        //alert("购物车 - 减005");
		var a = parseFloat($(this).siblings(".price").text());  //当前商品单价
		var n = parseInt($(this).next().text())-1;  //当前商品数量
        var s = parseFloat($("#totalpriceshow").text());  //总金额
        //var fn = parseInt(e.text()) - 1;  //商品列表数量
        var cid = $(this).parent().siblings().children(".accountName").attr("data-icon");  //获取data-icon 
        var qty = parseFloat($("h4[data-icon=" + cid + "]").parent().find(">.btn>.qty").text())-1;   //获取商品列表数量
        $("h4[data-icon=" + cid + "]").parent().find(">.btn>.qty").text(qty);   //更新菜单列表的数量
        var remark = $(this).parent().siblings().children(".taste").text();  //获取规格
        if (n == 0){
			$(this).parent().parent().remove();   
            $(".up1").hide();
            if (qty == 0)
            {
                $("h4[data-icon=" + cid + "]").parent().find(">.btn>.qty").css("display", "none")   //隐藏商品列表数量
                $("h4[data-icon=" + cid + "]").parent().find(">.btn>.minus").css("display", "none")  //隐藏商品列表减号
                //e.css("display", "none");
                //e.prev().css("display", "none")
            }
		}
		$(this).next().text(n);
  //      e.text(fn);    //赋值给商品列表的数量
        $(this).parent().prev().children("span:nth-child(2)").text((a * n).toFixed(2));
        //AJAX开始（商品列表减）##############################################################################
        $.ajax({
            url: "/OrderFood/ShopDelFood",    //方法
            type: 'post',
            async: false,
            data: { GoodsID: cid, OpenID: $(".openid").html(), Remark: remark},
            success: function (result) {
                $.hideLoading();
                if (result.statuscode == "200") {

                    $("#totalcountshow").text(parseInt($("#totalcountshow").text()) - 1);
                    $("#totalpriceshow").text((s * 1 - a * 1).toFixed(2));
                    if (parseFloat($("#totalcountshow").text()) == 0) {
                        $(".shopcart-list").hide();
                        jss();
                    }
                }
                if (result.statusCode == "300") {
                    $.toast(result.message, "警告");
                }
            }
        });
           //AJAX结束（商品列表减）##############################################################################

	});

    function jss() {
        var m = $("#totalcountshow").html();
        if (m > 0) {
            $(".right").find("a").removeClass("disable");
        } else {
            $(".right").find("a").addClass("disable");
        }
    };
	
    //选项卡
    $(".con>div").hide(); //con下所有都隐藏
    $(".con>div:eq(0)").show(); //显示第一个DIV
    $(".left-menu li").click(function () {
        $(this).addClass("active").siblings().removeClass("active");
        //var n = $(".left-menu li").index(this);
        //$(".left-menu li").index(this);
        //$(".con>div").hide();
        //$(".con>div:eq(" + n + ")").show();
        var ind = $(".left-menu li").index(this);
        var leftid = ($(this).attr('id'));
        //alert(ind);
        //alert(leftid);
        //$(".con li").eq(cin).show().siblings().hide();
        //$(".con li").$(leftid).show();
        //$(".con li").hide();
        $(".con li").hide();
        $("." + leftid + "").show();
    });

    $(".subFly").hide();
    $(".close").click(function(){
        $(".subFly").hide();
    });
    //$(document).on('click', '.footer', function () {
    // 点击购物车的显示和隐藏
    $(".footer>.left").click(function () {

        var lul = $(".list-content ul");
        //alert("点击购物车的显示和隐藏");
        var url = "/OrderFood/GetShopCartlistSH";
        $.getJSON(url, { 'OpenID': $('.openid').html(), 'ShopCode': $(".shopcode").html() }, function (data) {
            lul.find("li").remove();
            $.each(data.list, function (i, item) {
                var addtr = '<li class="food">';
                addtr += '<div><span class="accountName" data-icon="' + item.GoodsID + '">' + item.GoodsFullName + '</span><span class="taste">' + item.Remark + '</span></div>';
                addtr += '<div><span>￥</span><span class="accountPrice">' + item.Amount + '</span></div>';
                addtr += '<div class="btn">';
                addtr += '<button class="ms2" style="display: inline-block;"></button>';
                addtr += '<i class="li_acount">' + item.Qty + '</i>';
                addtr += '<button class="ad2"></button>';
                addtr += '<i class="price" style="display: none;">' + item.SJ + '</i>';
                addtr += '<i class="goodscode" style="display: none;">' + item.GoodsCode + '</i>';
                addtr += '</div>';
                addtr += '</li>';
                lul.append(addtr);
            })
        });
        var content = $(".list-content>ul").html();
        if (content != "") {
            $(".shopcart-list.fold-transition").toggle();
            $(".oldshopcart-list.fold-transition").hide();
            $(".up1").toggle();
        }
    });
    //点击已购商品的显示和隐藏
    $(".footer>.middle").click(function () {
        var url = "/OrderFood/GetMidOrderList";
        var shopcode = $(".shopcode").html();
        var zhuohao = $(".zhuohao").html();
        var openid = $(".openid").html();
        
        $.getJSON(url, { 'shopcode': shopcode, 'zhuohao': zhuohao, 'openid': openid }, function (data) {
            if (data.list.length == 0) {
                $('.oldlist-content').text("暂无数据");
            }
            else {
                var items = [];
                $('.oldlist-content').empty();
                $.each(data.list, function (i, item) {
                    if (item.goodscode != "")  //如果存在已购商品 则显示去结算按钮
                    {
                        $(".right").find("a").removeClass("disable");
                    }
                    items.push(' <li class="oldfood">');
                    items.push('<div><span class="oldaccountName">' + item.GOODSFULLNAME + '</span><span class="taste">' + item.REMARK + ' </span></div>');
                    items.push('<div><span>￥</span><span class="oldaccountPrice">' + item.AMOUNT + '</span></div>');
                    items.push('<div class="btn">');
                    items.push('<i style="display: none;"></i>')
                    items.push('<i class="oldli_acount">' + item.QTY + '</i>');
                    items.push('</div>');
                    items.push('</li > ');

                });
                $("<ul/>", { html: items.join("") }).appendTo(".oldlist-content");
            }
        });
        $(".shopcart-list.fold-transition").hide();
        $(".oldshopcart-list.fold-transition").toggle();
        $(".up1").toggle();
    });
    

    $(".up1").click(function(){
        $(".up1").hide();
        $(".shopcart-list.fold-transition").hide();
        $(".oldshopcart-list.fold-transition").hide();
    })

	//清空购物车
    $(".empty").click(function () {
        //AJAX开始（清空购物车）##############################################################################
        $.ajax({
            url: "/OrderFood/EmptyCart",    //方法
            type: 'post',
            async: false,
            data: { ShopCode: $(".shopcode").html(), OpenID: $(".openid").html()},
            success: function (result) {
                $.hideLoading();
                if (result.statuscode == "200") {
                    $(".list-content>ul").html("");
                    $("#totalcountshow").text("0");
                    $("#totalpriceshow").text("0");
                    $(".minus").next().text("0");
                    $(".minus").hide();
                    $(".minus").next().hide();
                    $(".shopcart-list").hide();
                    $(".oldshopcart-list").hide();
                    $(".up1").hide();
                    jss();//改变按钮样式
                }
                if (result.statusCode == "300") {
                    $.toast(result.message, "警告");
                }
            }
        });
           //AJAX结束（清空购物车）##############################################################################

    });
});
