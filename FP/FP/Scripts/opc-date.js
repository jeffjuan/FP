$((function ($) {
    $.datepicker.regional['zh-TW'] = {
        clearText: '清除',
        clearStatus: '清除已選日期',
        closeText: '關閉',
        closeStatus: '不改變當前選擇',
        prevText: '<上月',
        prevStatus: '顯示上月',
        prevBigText: '<<',
        prevBigStatus: '顯示上一年',
        nextText: '下月>',
        nextStatus: '顯示下月',
        nextBigText: '>>',
        nextBigStatus: '顯示下一年',
        currentText: '今天',
        currentStatus: '顯示本月',
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'],
        monthStatus: '選擇月份',
        yearStatus: '選擇年份',
        weekHeader: '週',
        weekStatus: '年內週次',
        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['週日', '週一', '週二', '週三', '週四', '週五', '週六'],
        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
        dayStatus: '設置DD為一周起始',
        dateStatus: '選擇m月d日, DD',
        dateFormat: 'yy-mm-dd',
        firstDay: 1,
        initStatus: '請選擇日期',
        isRTL: false

    };
    $.datepicker.setDefaults($.datepicker.regional['zh-TW']);
})(jQuery));


$(function () {
    $(".opc-datepicker").datepicker({
        option: new Date(),
        dateFormat: "yy/mm/dd",
        //改變顯示位置在最上層------start
        beforeShow: function (input) {
            $(input).css({
                "position": "relative",
                "z-index": 999999
            });
        }
        //改變顯示位置在最上層------end
    });

    var firstDate = new Date();
    firstDate.setDate(1); //Fist day
    var endDate = new Date(firstDate);
    endDate.setMonth(firstDate.getMonth() + 1);
    endDate.setDate(0); //Last day


    $("#StartAt").val($.datepicker.formatDate('yy/mm/dd', firstDate));
    $("#EndAt").val($.datepicker.formatDate('yy/mm/dd', endDate));
    //$('input[name="Quote.EffectiveDate"]').val($.datepicker.formatDate('yy/mm/dd', endDate));

    $("#StartAt02").val($.datepicker.formatDate('yy/mm/dd', firstDate));
    $("#EndAt02").val($.datepicker.formatDate('yy/mm/dd', endDate));
    //input-validation-error
    //報表日期驗證(edit時)
    $("#StartAt, #EndAt").change("keypress", function () {
        var StartAt = [$("#StartAt").val(), "#StartAt"];
        var EndAt = [$("#EndAt").val(), "#EndAt"];

        if (!date_array(StartAt)[0]) {
            $(date_array(StartAt)[1]).css('border', '1px solid red');
            $("#error_s").css({ 'color': 'red' }).html("日期格式需為YYYY/MM/DD");
        } else {
            $(date_array(StartAt)[1]).removeAttr('style');
            $("#error_s").html("");
        }

        if (!date_array(EndAt)[0]) {
            $(date_array(EndAt)[1]).css('border', '1px solid red');
            $("#error_e").css({ 'color': 'red' }).html("日期格式需為YYYY/MM/DD");
        } else {
            $(date_array(EndAt)[1]).removeAttr('style');
            $("#error_e").html("");
        }

    });

    //報表日期驗證(送出時)
    $("#Report_S").submit(function (e) {
        var array_dade = [$("#StartAt").val(), $("#EndAt").val()];
        var check = true;
        for (var i = 0; i < array_dade.length; i++) {
            //跑第一次
            if (dateValidationCheck(array_dade[i]) == false) {
                check = false;
                break;
            }
        }
        if (!check) { //--跑迴function
            //alert('日期格式為YYYY/MM/DD，請重新確認');
            return false;
        } else {
            return true;
        }
    });

    function date_array(array_dade) {
        var array_check = [true, null];
        if (dateValidationCheck(array_dade[0]) == false) {
            array_check[0] = false;
        }
        array_check[1] = array_dade[1];
        return array_check;
    }

    function dateValidationCheck(str) {
        //跑第二次
        var re = new RegExp("^([0-9]{4})[./]{1}([0-9]{1,2})[./]{1}([0-9]{1,2})$");
        var strDataValue;
        var infoValidation = true;
        if ((strDataValue = re.exec(str)) != null) {
            var i;
            i = parseFloat(strDataValue[1]);
            if (i <= 0 || i > 9999) { // 年
                infoValidation = false;
            }
            i = parseFloat(strDataValue[2]);
            if (i <= 0 || i > 12) { // 月
                infoValidation = false;
            }
            i = parseFloat(strDataValue[3]);
            if (i <= 0 || i > 31) { // 日
                infoValidation = false;
            }
        } else {
            infoValidation = false;
        }
        return infoValidation;
    }

});