(function ($) {
    //Ϊjquery��չһ��menu����
    $.fn.menu = function (b) {
        //������������
        var c, item, httpAdress;
        //��չ�����������������ԣ����b�в������滻���������ԣ�û�о���Ĭ�ϵ�
        b = jQuery.extend({
            Speed: 220,
            autostart: 1,
            autohide: 1,
            type:0,
        },
        b);
     
        //��ǰ���÷�����ʵ��
        c = $(this);

        item = c.children("ul").parent("li").find("div[name='pic_name']");
        //�������ڻ�õ�ǰҳ��ĵ�ַ (URL)������������ض����µ�ҳ�档
        httpAdress = window.location;

        if (b.type == 0) {

            item.each(function () {

                var ul = $(this).parent().children("ul");

                if (ul.length > 0) {

                    $(this).addClass("inactive");

                    ul.hide();

                }

            });

        }
        else {

            item.each(function () {

                var e = $(this);

                if (e.attr("class") == "active") {

                    e.parent().children("ul").show();

                }
                else {

                    e.parent().children("ul").hide();
                }

            })

        }
        
        function _item() {

            var a = $(this);

            if (b.autohide) {

                a.parent().parent().find(".active").parent("li").children("ul").slideUp(b.Speed / 1.2, 
                function () {
                    var a = $(this).parent("li").find("div[name='pic_name']");

                    a.removeAttr("class");

                    a.attr("class", "inactive");
                   
                })
            }
            if (a.attr("class") == "inactive") {
                
                a.parent("li").children("ul").slideDown(b.Speed, 
                function() {
                    a.removeAttr("class");

                    a.addClass("active");

                    var sub_item = a.parent().children("ul").find("li");

                    sub_item.each(function () {

                        var sub = $(this).find("div[name='pic_name']");

                        sub.removeAttr("class");

                        if (sub.parent().children("ul").find("li").length > 0) {

                            sub.addClass("inactive");

                        }

                    });

                })
            }
            if (a.attr("class") == "active") {
                a.removeAttr("class");
                a.addClass("inactive");
                a.parent("li").children("ul").slideUp(b.Speed,
                    function () {

                        var sub_item = a.children("ul").parent("li").find("div[name='pic_name']");

                        sub_item.each(function () {

                            var sub = $(this);

                            sub.removeAttr("class");

                            if (sub.children("ul").length>0) {

                                sub.addClass("inactive");

                            } 
                            

                        });

                        var ul = a.children("ul");

                        if (ul.length > 0) {

                            a.addClass("active");

                        }
                        
                })
            }
        }
        //�Ƴ�����Ԫ�ص��¼�������
        item.unbind('click').click(_item);
        //if (b.autostart) {
        //    c.find(".pic_a").each(function () {
        //        if (this.href == httpAdress) {
        //            $(this).parent("li").parent("ul").slideDown(b.Speed, 
        //            function() {
        //                $(this).parent("li").children(".inactive").removeAttr("class");
        //                $(this).parent("li").find(".pic_a").addClass("active")
        //            })
        //        }
        //    })
        //}
    }
})(jQuery);