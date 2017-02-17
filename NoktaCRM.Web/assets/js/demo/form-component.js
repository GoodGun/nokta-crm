$(document).ready(function () {
    $(".demo-panel-ref-btn").jasmineOverlay().on("click", function () {
        var b, a = $(this);
        a.jasmineOverlay("show"), b = setInterval(function () {
            a.jasmineOverlay("hide"), clearInterval(b)
        }, 2e3)
    }), $("[data-click=panel-expand]").click(function (a) {
        a.preventDefault();
        var b = $(this).closest(".panel");
        $("body").hasClass("panel-expand") && $(b).hasClass("panel-expand") ? ($("body, .panel").removeClass("panel-expand"), $(".panel").removeAttr("style")) : ($("body").addClass("panel-expand"), $(this).closest(".panel").addClass("panel-expand")), $(window).trigger("resize")
    }), $("[data-click=panel-collapse]").click(function (a) {
        a.preventDefault(), $(this).closest(".panel").find(".panel-body").slideToggle()
    }), $("[data-click=panel-reload]").click(function (a) {
        a.preventDefault();
        var b = $(this).closest(".panel");
        if (!$(b).hasClass("panel-loading")) {
            var c = $(b).find(".panel-body"),
                d = '<div class="panel-loader"><span class="spinner-small"></span></div>';
            $(b).addClass("panel-loading"), $(c).prepend(d), setTimeout(function () {
                $(b).removeClass("panel-loading"), $(b).find(".panel-loader").remove()
            }, 2e3)
        }
    }), $("#jquery-tagIt-default").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $("#jquery-tagIt-inverse").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $("#jquery-tagIt-white").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $("#jquery-tagIt-primary").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $("#jquery-tagIt-info").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $("#jquery-tagIt-success").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $("#jquery-tagIt-warning").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $("#jquery-tagIt-danger").tagit({
        availableTags: ["tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7"]
    }), $(".demo-chosen-select").chosen({
        width: "100%"
    }), $(".demo-cs-multiselect").chosen({
        width: "100%"
    }),
    //$("#demo-range-def").noUiSlider({
    //    start: [20],
    //    connect: "lower",
    //    range: {
    //        min: [0],
    //        max: [100]
    //    }
    //}).Link("lower").to($("#demo-range-def-val")), $("#demo-range-step").noUiSlider({
    //    start: [20],
    //    connect: "lower",
    //    step: 10,
    //    range: {
    //        min: [0],
    //        max: [100]
    //    }
    //}).Link("lower").to($("#demo-range-step-val")), $("#demo-range-ver1").noUiSlider({
    //    start: [80],
    //    connect: "lower",
    //    range: {
    //        min: [20],
    //        max: [100]
    //    },
    //    orientation: "vertical",
    //    direction: "rtl"
    //}), $("#demo-range-ver2").noUiSlider({
    //    start: [50],
    //    connect: "lower",
    //    range: {
    //        min: [20],
    //        max: [100]
    //    },
    //    orientation: "vertical",
    //    direction: "rtl"
    //}), $("#demo-range-ver3").noUiSlider({
    //    start: [30],
    //    connect: "lower",
    //    range: {
    //        min: [20],
    //        max: [100]
    //    },
    //    orientation: "vertical",
    //    direction: "rtl"
    //}), $("#demo-range-ver4").noUiSlider({
    //    start: [50],
    //    connect: "lower",
    //    range: {
    //        min: [20],
    //        max: [100]
    //    },
    //    orientation: "vertical",
    //    direction: "rtl"
    //}), $("#demo-range-ver5").noUiSlider({
    //    start: [80],
    //    connect: "lower",
    //    range: {
    //        min: [20],
    //        max: [100]
    //    },
    //    orientation: "vertical",
    //    direction: "rtl"
    //}), $("#demo-range-drg").noUiSlider({
    //    start: [40, 60],
    //    behaviour: "drag",
    //    connect: !0,
    //    range: {
    //        min: 20,
    //        max: 80
    //    }
    //}), $("#demo-range-fxt").noUiSlider({
    //    start: [40, 60],
    //    behaviour: "drag-fixed",
    //    connect: !0,
    //    range: {
    //        min: 20,
    //        max: 80
    //    }
    //}), $("#demo-range-com").noUiSlider({
    //    start: [40, 60],
    //    behaviour: "drag-tap",
    //    connect: !0,
    //    range: {
    //        min: 20,
    //        max: 80
    //    }
    //});
    //var a = {
    //    min: [0],
    //    "25%": [50],
    //    "50%": [100],
    //    "75%": [150],
    //    max: [200]
    //};
    //$("#demo-range-hpips").noUiSlider({
    //    range: a,
    //    connect: "lower",
    //    start: 90
    //}), $("#demo-range-vpips").noUiSlider({
    //    range: a,
    //    start: 90,
    //    connect: "lower",
    //    orientation: "vertical",
    //    direction: "rtl"
    //}), $(".demo-pips").noUiSlider_pips({
    //    mode: "range",
    //    density: 5
    //}), $("#default_rangeSlider").ionRangeSlider({
    //    min: 0,
    //    max: 5e3,
    //    type: "double",
    //    prefix: "$",
    //    maxPostfix: "+",
    //    prettify: !1,
    //    grid: !0
    //}), $("#customRange_rangeSlider").ionRangeSlider({
    //    min: 1e3,
    //    max: 1e5,
    //    from: 3e4,
    //    to: 9e4,
    //    type: "double",
    //    step: 500,
    //    postfix: " €",
    //    grid: !0
    //}), $("#customValue_rangeSlider").ionRangeSlider({
    //    values: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
    //    type: "single",
    //    grid: !0
    //}), 
    $("#demo-tp-com").timepicker(), $("#demo-tp-textinput").timepicker({
        minuteStep: 5,
        showInputs: !1,
        disableFocus: !0
    }), $("#demo-dp-txtinput input").datepicker(), $("#demo-dp-component .input-group.date").datepicker({
        autoclose: !0
    }), $("#demo-dp-range .input-daterange").datepicker({
        format: "dd.mm.yyyy",
        todayBtn: "linked",
        autoclose: !0,
        todayHighlight: !0
    }), $("#demo-dp-inline div").datepicker({
        format: "dd.mm.yyyy",
        todayBtn: "linked",
        autoclose: !0,
        todayHighlight: !0
    }), Dropzone.options.demoDropzone = {
        autoProcessQueue: !1,
        init: function () { }
    }, $("#demo-summernote").summernote({
        height: 250
    }), $("#demo-msk-date").mask("99/99/9999"), $("#demo-msk-date2").mask("99-99-9999"), $("#demo-msk-phone").mask("(999) 999-9999"), $("#demo-msk-taxid").mask("99-9999999"), $("#demo-msk-ssn").mask("999-99-9999"), $("#demo-msk-pkey").mask("aaaa*-aaaa*-aaaa*-aaaa*-aaaa*"), $("#demo-msk-currency").mask("$ 999,999,999.99"), $("#demo-msk-ipv6").mask("9999:9999:9999:9:999:9999:9999:9999"), $("#demo-msk-ipv4").mask("999.999.999.999"), $("#demo-msk-isbn2").mask("999/99/999/9999/9"), $("#demo-msk-isbn1").mask("999-99-999-9999-9")
});