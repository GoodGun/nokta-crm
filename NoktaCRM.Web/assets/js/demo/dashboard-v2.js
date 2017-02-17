$(document).ready(function () {
    function d() {
        var a = $("#sortable li").length;
        $(".count-todos").html(a)
    }

    function e(a) {
        var b = '<li class="ui-state-default"><div class="checkbox"><label class="form-checkbox form-icon"><input type="checkbox"/>' + a + "</label></div></li>";
        $("#sortable").append(b), $(".add-todo").val("")
    }

    function f(a) {
        var b = a,
            c = "<li>" + b + '<a class="remove-item fa fa-remove pull-right" href="#" role="button"></a></li>';
        $("#done-items").append(c), $(".remove").remove()
    }

    function g() {
        var a = [];
        for ($("#sortable li").each(function () {
                a.push($(this).text())
        }), i = 0; i < a.length; i++) $("#done-items").append("<li>" + a[i] + '<a class="remove-item fa fa-remove pull-right" href="#" role="button"></a></li>');
        $("#sortable li").remove(), d()
    }

    function h(a) {
        $(a).parent().remove()
    }
    //$("#world-map-markers").vectorMap({
    //    map: "world_mill_en",
    //    scaleColors: ["#C8EEFF", "#0071A4"],
    //    normalizeFunction: "polynomial",
    //    hoverOpacity: .7,
    //    hoverColor: !1,
    //    zoomButtons: !1,
    //    markerStyle: {
    //        initial: {
    //            fill: "#fad733"
    //        },
    //        selected: {
    //            fill: "#ffffff"
    //        }
    //    },
    //    regionStyle: {
    //        initial: {
    //            fill: "#23b7e5"
    //        },
    //        selected: {
    //            fill: "#27c24c"
    //        }
    //    },
    //    series: {
    //        markers: [{
    //            attribute: "r",
    //            scale: [5, 15],
    //            values: [187.7, 255.16, 310.69, 605.17, 248.31, 107.35, 217.22]
    //        }]
    //    },
    //    backgroundColor: "#fff",
    //    regionsSelectable: !0,
    //    markersSelectable: !0,
    //    markers: [{
    //        latLng: [41.9, 12.45],
    //        name: "Vatican City"
    //    }, {
    //        latLng: [55.75, 37.61],
    //        name: "Moscow"
    //    }, {
    //        latLng: [52.52, 13.4],
    //        name: "Berlin"
    //    }, {
    //        latLng: [37.77, -122.41],
    //        name: "San Francisco"
    //    }, {
    //        latLng: [3.2, 73.22],
    //        name: "Maldives"
    //    }, {
    //        latLng: [32.71, -117.16],
    //        name: "San Diego"
    //    }, {
    //        latLng: [40.71, -74],
    //        name: "New York"
    //    }, {
    //        latLng: [47.6, -122.33],
    //        name: "Seattle"
    //    }, {
    //        latLng: [1.3, 103.8],
    //        name: "Singapore"
    //    }, {
    //        latLng: [41.87, -87.62],
    //        name: "Chicago"
    //    }, {
    //        latLng: [26.02, 50.55],
    //        name: "Bahrain"
    //    }, {
    //        latLng: [43.73, 7.41],
    //        name: "Monaco"
    //    }, {
    //        latLng: [-.52, 166.93],
    //        name: "Nauru"
    //    }, {
    //        latLng: [-8.51, 179.21],
    //        name: "Tuvalu"
    //    }, {
    //        latLng: [43.93, 12.46],
    //        name: "San Marino"
    //    }, {
    //        latLng: [47.14, 9.52],
    //        name: "Liechtenstein"
    //    }, {
    //        latLng: [40.71, -74],
    //        name: "New York"
    //    }, {
    //        latLng: [29.76, -95.36],
    //        name: "Houston"
    //    }, {
    //        latLng: [1.46, 173.03],
    //        name: "Kiribati"
    //    }, {
    //        latLng: [-21.13, -175.2],
    //        name: "Tonga"
    //    }, {
    //        latLng: [15.3, -61.38],
    //        name: "Dominica"
    //    }]
    //});
    //var a = [
    //        ["Jan", 5],
    //        ["Feb", 8],
    //        ["March", 6],
    //        ["April", 9],
    //        ["May", 7],
    //        ["June", 4],
    //        ["July", 8],
    //        ["Aug", 12],
    //        ["Sept", 6],
    //        ["Oct", 8],
    //        ["Nov", 6],
    //        ["Dec", 10]
    //],
    //    c = ($.plot("#demo-earning-statistics", [{
    //        label: "Net Earning",
    //        data: a
    //    }], {
    //        series: {
    //            lines: {
    //                show: !0,
    //                lineWidth: 2,
    //                fill: !1
    //            },
    //            points: {
    //                show: !0,
    //                lineWidth: 2,
    //                fill: !0,
    //                fillColor: "#ffffff",
    //                symbol: "circle",
    //                radius: 5
    //            },
    //            shadowSize: 0
    //        },
    //        colors: ["#248aaf"],
    //        tooltip: !0,
    //        tooltipOpts: {
    //            defaultTheme: !1
    //        },
    //        legend: {
    //            show: !0,
    //            position: "nw",
    //            margin: [15]
    //        },
    //        grid: {
    //            hoverable: !0,
    //            clickable: !0,
    //            tickColor: "#eeeeee",
    //            borderWidth: 1,
    //            borderColor: "#eeeeee"
    //        },
    //        xaxis: {
    //            mode: "categories"
    //        }
    //    }), [
    //        ["Jan", 6],
    //        ["Feb", 12],
    //        ["March", 10],
    //        ["April", 8],
    //        ["May", 12],
    //        ["June", 10],
    //        ["July", 8],
    //        ["Aug", 12],
    //        ["Sept", 6],
    //        ["Oct", 8],
    //        ["Nov", 6],
    //        ["Dec", 10]
    //    ]);
    //$.plot("#demo-sales-statistics", [{
    //    label: "New Orders",
    //    data: c
    //}], {
    //    series: {
    //        lines: {
    //            show: !0,
    //            lineWidth: 2,
    //            fill: !0
    //        },
    //        points: {
    //            show: !0,
    //            lineWidth: 2,
    //            fill: !0,
    //            fillColor: "#ffffff",
    //            symbol: "circle",
    //            radius: 5
    //        },
    //        shadowSize: 0
    //    },
    //    colors: ["#29b7d3;"],
    //    tooltip: !0,
    //    tooltipOpts: {
    //        defaultTheme: !1
    //    },
    //    legend: {
    //        show: !0,
    //        position: "nw",
    //        margin: [15]
    //    },
    //    grid: {
    //        hoverable: !0,
    //        clickable: !0,
    //        tickColor: "#eeeeee",
    //        borderWidth: 1,
    //        borderColor: "#eeeeee"
    //    },
    //    xaxis: {
    //        mode: "categories"
    //    }
    //});
    $("#sortable").sortable(), $("#sortable").disableSelection(), d(), $("#checkAll").click(function () {
        g()
    }), $(".add-todo").on("keypress", function (a) {
        if (a.preventDefault, 13 == a.which && "" != $(this).val()) {
            var b = $(this).val();
            e(b), d()
        }
    }), $(".todolist").on("change", '#sortable li input[type="checkbox"]', function () {
        if ($(this).prop("checked")) {
            var a = $(this).parent().parent().find("label").text();
            $(this).parent().parent().parent().addClass("remove"), f(a), d()
        }
    }), $(".todolist").on("click", ".remove-item", function () {
        h(this)
    })
});