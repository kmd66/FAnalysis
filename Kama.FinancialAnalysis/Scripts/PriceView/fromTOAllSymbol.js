const _itemStyle = {
    color: '#00da3c',
    color0: '#ec0000',
    borderColor: undefined,
    borderColor0: undefined
}

async function initCahrt(_obj,divId) {
    var dom = document.getElementById(divId);
    var myChart = echarts.init(dom, null, {
        renderer: 'canvas',
        useDirtyRect: false
    });
    var option;
    option = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'cross'
            },
            position: function (pos, params, el, elRect, size) {
                var obj = { top: 10 };
                obj[['left', 'right'][+(pos[0] < size.viewSize[0] / 2)]] = 30;
                return obj;
            },
            xAxisIndex: [0, 1],
        },
        xAxis: [
            {
                type: 'category',
                data: getTimes(_obj)
            }
        ],
        yAxis: [{ scale: true }],
        series: [
            {
                type: 'candlestick',
                itemStyle: _itemStyle,
                data: getOhlc(_obj, true),
            }
        ]
    };

    if (option && typeof option === 'object') {
        myChart.setOption(option);
    }

    window.addEventListener('resize', myChart.resize);

}

function getData() {
    var t = document.getElementById('dateInput').value;
    $("#dateInputDiv").hide();
    $.post("/PriceView/FromTOAllSymbol", { FromDate: t })
        .done(function (data) {
            var _obj = data.Data;

            for (var i = 0; i < _obj.length; i++) {
                initCahrt(_obj[i].reverse(), `container${i}`) 
                
            }
        })
        .fail(function (e) {
            alert("error" + e);
        })
        .always(function (e) {
            $("#dateInputDiv").show();
        });
}

function getTimes(_obj) {
    var obj = [];
    for (var i = 0; i < _obj.length; i++) {
        var d = new Date(parseInt(_obj[i].Date.match(/\d+/)[0] * 1));
        obj.push(moment(d).format('L') + ' ' + moment(d).format('LT'));
    }
    return obj;
}

function getOhlc(_obj, b) {
    var obj = [];
    if (b)
        for (var i = 0; i < _obj.length; i += 1) {
            obj.push([
                _obj[i].Open,
                _obj[i].Close,
                _obj[i].Min,
                _obj[i].Max
            ]);
        }
    else
        for (var i = 0; i < _obj.length; i += 1) {
            obj.push(_obj[i].Close);
        }
    return obj;
}

