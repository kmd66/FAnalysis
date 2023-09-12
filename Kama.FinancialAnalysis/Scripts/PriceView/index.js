var _obj;
var initSession = [];
var _pageIndex = 1;

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

    option = {
        legend: {
            data: ['MA5', 'MA', 'SDR100', 'SDR500', 'SDR1000']
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
        series: [
            {
                name: 'MA5',
                type: 'candlestick',
                data: getOhlc(),
                xAxisIndex: 0, yAxisIndex: 0,
            },


            {
                name: 'bar1',
                type: 'bar',
                xAxisIndex: 0, yAxisIndex: 0,

                markPoint: {
                    data: [
                        {
                            name: 'Mark',
                            coord: [time[563], 1.07163],
                            xAxisIndex: 0, yAxisIndex: 0,
                            value: 1
                        },
                    ],
                    xAxisIndex: 0, yAxisIndex: 0,
                },
                markArea: {
                    itemStyle: {
                        color: 'rgba(255, 173, 177, 0.4)'
                    },
                    data: [
                        [
                            {
                                name: 'Morning Peak',
                                color: "blue",
                                xAxis: time[363]
                            },
                            {
                                color: "blue",
                                xAxis: time[770]
                            }
                        ],
                    ]
                },

            },
            {
                name: 'bar2',
                type: 'bar',
                xAxisIndex: 0, yAxisIndex: 0,
                markArea: {
                    itemStyle: {
                        color: 'rgba(255, 255, 177, 0.4)'
                    },
                    data: [
                        [
                            {
                                name: 'aaaaaaaa',
                                xAxis: time[590], color: 'rgba(255, 255, 177, 0.4)'
                            },
                            {
                                xAxis: time[970]
                            }
                        ],
                    ]
                },
                markLine: {
                    silent: true, // ignore mouse events
                    data: [
                        // Horizontal Axis (requires valueIndex = 0)
                        { type: 'average', name: 'Marker Line', valueIndex: 1, xAxis: time[563], itemStyle: { normal: { color: '#1e90ff' } } },
                    ]
                },
            },

            movingAverages('M5'),
            movingAverages('M30'),
            movingAverages('H1'),
            movingAverages('D'),
            standardDeviation('R100'),
            standardDeviation('R500'),
            standardDeviation('R1000'),
        ]
    };

    if (option && typeof option === 'object') {
        myChart.setOption(option);
    }

    window.addEventListener('resize', myChart.resize);

}

function getData(p) {
    _pageIndex = _pageIndex + p;
    $.post("PriceView/ListView", { Type: 1, pageIndex: _pageIndex })
        .done(function (data) {
            _obj = data.Data;
            _obj.Bases = _obj.Bases.reverse();
            _obj.MovingAverages = _obj.MovingAverages.reverse();
            _obj.StandardDeviations = _obj.StandardDeviations.reverse();
            initCahrt();
        })
        .fail(function (e) {
            alert("error" + e);
        });
}

function getTimes() {
    var obj = [];
    for (var i = 0; i < _obj.Bases.length; i++) {
        var d = new Date(parseInt(_obj.Bases[i].Date.match(/\d+/)[0] * 1));
        obj.push(d.toISOString().replace(':00.000Z', '').replace('T', ' '));
    }
    return obj;
}

function getOhlc() {
    var obj = [];
    for (var i = 0; i < _obj.Bases.length; i += 1) {
        if (i == 563)
            rt = _obj.Bases[i].Max
        obj.push([
            _obj.Bases[i].Open,
            _obj.Bases[i].Close,
            _obj.Bases[i].Min,
            _obj.Bases[i].Max
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
        for (var i = 0; i < _obj.MovingAverages.length; i += 1) {
            obj.push(_obj.MovingAverages[i][type]);
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
        for (var i = 0; i < _obj.StandardDeviations.length; i += 1) {
            obj.push(_obj.StandardDeviations[i][type]);
        }
        return obj;
    }
};

function initSessionObj() {
    if (initSession.length > 0) {

    }

    return []
};
