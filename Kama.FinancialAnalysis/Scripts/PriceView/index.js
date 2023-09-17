var _obj;
var initSession = [];
var _pageIndex = 1;
var _sessions;
var _series = [];

getData(0);


async function initCahrt() {

    var dom = document.getElementById('container');
    var myChart = echarts.init(dom, null, {
        renderer: 'canvas',
        useDirtyRect: false
    });
    var option;
    var rt=1
    var time = getTimes();
    initChartService();

    option = {
        legend: {
            data: ['session', 'MA5', 'MA', 'SDR1000']
        },
        grid: [{
            bottom: '23%',
        }, {
            top: '78%',
            bottom: '15%'
        }],
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
                data: time,
                boundaryGap: false,
                splitLine: { show: false },
                axisLine: { show: false },
                axisTick: { show: false },
                axisLabel: { show: false },

                min: 'dataMin',
                max: 'dataMax',
                hideOverlap: true,
                axisPointer: {
                    z: 0
                }, gridIndex: 0
            },
            {
                data: time, type: 'category', gridIndex: 1
            }
        ],
        dataZoom: [
            {
                type: 'inside',
                xAxisIndex: [0, 1],
                start: 0,
                end: 100,
            },
            {
                show: true,
                xAxisIndex: [0, 1],
                type: 'slider',
                bottom: 60,
                start: 0,
                end: 100,
            }
        ],
        yAxis: [{
            scale: true, gridIndex: 0
        }, {
            scale: true, gridIndex: 1
            }],
        series: _series
    };

    if (option && typeof option === 'object') {
        myChart.setOption(option);
    }

    window.addEventListener('resize', myChart.resize);

}

function initChartService() {
    var time = getTimes();
    _series= [
        {
            name: 'MA5',
            type: 'candlestick',
            data: getOhlc(),
            xAxisIndex: 0, yAxisIndex: 0,
        },
        movingAverages('M5'),
        movingAverages('M30'),
        movingAverages('H1'),
        movingAverages('D'),
        //standardDeviation('R100'),
        //standardDeviation('R500'),
        standardDeviation('R1000'),
    ]

    var found1 = _obj.find((item) => item.Session == 1);
    if (found1) {
        _series.push(
            {
                name: 'session',
                type: 'bar',
                xAxisIndex: 0, yAxisIndex: 0,

                markLine: {
                    silent: true, // ignore mouse events
                    data: [
                        { type: 'average', name: 'Marker Line', valueIndex: 1, xAxis: getTimesByID(found1.ID), itemStyle: { normal: { color: '#1e90ff' } } },
                    ]
                },
            }
        );
    }
    addArea(10, 20, '#ff00a340', 'new york');
    addArea(11, 21, '#00ff8b40', 'london');
    //addArea(12, 22, '#ffed0040', 'sydney');
    function addArea(type1, type2, color, title) {

        var found10 = _obj.find((item) => item.Session == type1);
        var found20 = _obj.find((item) => item.Session == type2);
        if (found10 && found20) {
            _series.push(
                {
                    name: 'session',
                    type: 'bar',
                    xAxisIndex: 0, yAxisIndex: 0,

                    markArea: {
                        itemStyle: {
                            color: color
                        },
                        data: [
                            [
                                {
                                    name: title,
                                    xAxis: getTimesByID(found10.ID)
                                },
                                {
                                    xAxis: getTimesByID(found20.ID)
                                }
                            ],
                        ]
                    }
                }
            );
        }
    }

}

function getData(p) {
    var pageIndexSpan = document.getElementById('PageIndex');
    _pageIndex = _pageIndex + p;
    pageIndexSpan.innerHTML = _pageIndex;
    $.post("PriceView/ListView", { Type: 1, pageIndex: _pageIndex })
        .done(function (data) {
            _obj = data.Data;
            _obj = _obj.reverse();
            _sessions = _obj.filter((item) => item.Session > 0);
            var found = _obj.find((item) => item.Session > 100);
            //_obj.MovingAverages = _obj.MovingAverages.reverse();
            //_obj.StandardDeviations = _obj.StandardDeviations.reverse();
            initCahrt();
        })
        .fail(function (e) {
            alert("error" + e);
        });
}

function getTimes() {
    var obj = [];
    for (var i = 0; i < _obj.length; i++) {
        var d = new Date(parseInt(_obj[i].Date.match(/\d+/)[0] * 1));
        obj.push(moment(d).format('L') + ' ' + moment(d).format('LT'));
    }
    return obj;
}

function getTimesByID(id) {
    var item = _obj.find((x) => x.ID == id);
    var d = new Date(parseInt(item.Date.match(/\d+/)[0] * 1));
    return moment(d).format('L') + ' ' + moment(d).format('LT');
}

function getOhlc() {
    var obj = [];
    for (var i = 0; i < _obj.length; i += 1) {
        if (i == 563)
            rt = _obj[i].Max
        obj.push([
            _obj[i].Open,
            _obj[i].Close,
            _obj[i].Min,
            _obj[i].Max
        ]);
    }
    return obj;
}

function movingAverages(t) {
    var color;
    switch (t) {
        case 'M5':
            color = '#00ff0a'
            break;
        case 'M30':
            color = '#0006ff'
            break;
        case 'D':
            color = '#3dff00'
            break;
        case 'H1':
        default:
            color = '#ff0000'
            break;
    }

    return {
        name: 'MA',
        type: 'line',
        data: getData(t),
        smooth: true,
        showSymbol: false,
        lineStyle: {
            opacity: 0.5
        },
        color: color,
        xAxisIndex: 0, yAxisIndex: 0

    }

    function getData(type) {
        var obj = [];
        for (var i = 0; i < _obj.length; i += 1) {
            obj.push(_obj[i][type]);
        }
        return obj;
    }
};

function standardDeviation(t) {
    var color;
    switch (t) {
        case 'R100':
            color = '#00ff80'
            break;
        case 'R500':
            color = '#0011ff'
            break;
        case 'R1000':
        default:
            color = '#ff0021'
            break;
    }

    return {

        name: ('SD' + t),
        type: 'line',
        data: getData(t),
        smooth: true,
        showSymbol: false,
        lineStyle: {
            opacity: 0.5
        },
        color: color,
        xAxisIndex: 1, yAxisIndex: 1
    }

    function getData(type) {
        var obj = [];
        for (var i = 0; i < _obj.length; i += 1) {
            obj.push(_obj[i][type]);
        }
        return obj;
    }
};

function initSessionObj() {
    if (initSession.length > 0) {

    }

    return []
};
