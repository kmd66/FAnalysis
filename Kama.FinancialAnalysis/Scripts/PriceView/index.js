const _itemStyle = {
    color: '#00da3c',
    color0: '#ec0000',
    borderColor: undefined,
    borderColor0: undefined
}

var _type;
var _obj;
var initSession = [];
var _pageIndex;
var _sessions;
var _series = [];
var _biggerThanSD = [];
var _time;

setType(parseInt(document.getElementById('ViewBagID').value));

function setType(t) {
    var typeSpan = document.getElementById('TypeSpan');
    switch (t) {
        case 2:
            typeSpan.innerHTML = 'xauusd';
            break;
        case 3:
            typeSpan.innerHTML = 'usdchf';
            break;
        case 4:
            typeSpan.innerHTML = 'eurjpy';
            break;
        case 10:
            typeSpan.innerHTML = 'nq100m';
            break;
        default:
            typeSpan.innerHTML = 'DYX';
    }
    _type = t;
    _pageIndex = 1;
    getData(0);
}

var dom = document.getElementById('container');
var myChart = echarts.init(dom, null, {
    renderer: 'canvas',
    useDirtyRect: false
});


async function initCahrt() {
    var option;
    var rt=1
    _time = getTimes();
    initChartService();

    option = {
        legend: {
            data: ['session', 'MA5', 'MA', 'SD']
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
                data: _time,
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
                data: _time, type: 'category', gridIndex: 1
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
    _series= [
        {
            name: 'MA5',
            type: 'candlestick',
            data: getOhlc(),
            itemStyle: _itemStyle,
            xAxisIndex: 0, yAxisIndex: 0,
        },
        //movingAverages('M5'),
        //movingAverages('M30'),
        //movingAverages('H1'),
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

    //addArea(10, 20, '#ff00a340', 'new york');
    //addArea(11, 21, '#00ff8b40', 'london');
    //addArea(12, 22, '#ffed0040', 'sydney');

    addbiggerThanSD(110, '#ff00a3', 'newYork');
    addbiggerThanSD(111, '#0064ff', 'london');
    addbiggerThanSD(112, '#ffed00', 'london');

    //addMaxMin(110, '#ff00a3', 'newYork');
    //addMaxMin(111, '#0064ff', 'london');


    //#ff00a3
    //00ff8b

    function addbiggerThanSD(type, color) {
        if (_biggerThanSD) {

            biggerThanSD = _biggerThanSD.filter((item) => item.BiggerThanSD.Session == type);
            if (biggerThanSD.length>0) {

                var objBiggerThanSD = [];
                for (var i = 0; i < biggerThanSD.length; i += 1) {
                    objBiggerThanSD.push([
                        getTimesByID(biggerThanSD[i].ID),
                        (biggerThanSD[i].Close + biggerThanSD[i].Open) / 2
                    ]);
                }
                _series.push(
                    {
                        name: 'SD',
                        symbolSize: 14,
                        type: 'scatter',
                        xAxisIndex: 0, yAxisIndex: 0,
                        data: objBiggerThanSD,
                        color: color
                    }
                );
            }
        }
    }

    function addMaxMin(type, color) {
        if (_biggerThanSD) {

            biggerThanSD = _biggerThanSD.filter((item) => item.BiggerThanSD.Session == type);
            if (biggerThanSD.length > 0) {
                var objMaxMin = [];

                var filtered = biggerThanSD.filter(function ({ BiggerThanSD }) {
                    var key = `${BiggerThanSD['MaxPriceID']}`;
                    return !this.has(key) && this.add(key);
                }, new Set);

                var filteredMin = biggerThanSD.filter(function ({ BiggerThanSD }) {
                    var key = `${BiggerThanSD['MinPriceID']}`;
                    return !this.has(key) && this.add(key);
                }, new Set);

                for (var i = 0; i < filtered.length; i += 1) {
                    var found = _obj.find((item) => item.ID == filteredMin[i].BiggerThanSD.MaxPriceID);
                    if (found)
                    objMaxMin.push({
                        type: 'min',
                        name: 'Mark',
                        coord: [getTimesByID(filtered[i].BiggerThanSD.MaxPriceID), filtered[i].Close],
                        xAxisIndex: 0, yAxisIndex: 0,
                        value: 'Max'
                    });
                }
                for (var i = 0; i < filteredMin.length; i += 1) {
                    var found = _obj.find((item) => item.ID == filteredMin[i].BiggerThanSD.MinPriceID);
                    if (found)
                    objMaxMin.push({
                        name: 'Mark',
                        coord: [getTimesByID(found.ID), found.Close],
                        type: 'min',
                        value: 'Min'
                    });
                }
                _series.push({
                    name: 'SD',
                    type: 'bar',
                    xAxisIndex: 0, yAxisIndex: 0,

                    markPoint: {
                        data: objMaxMin,
                        itemStyle: {
                            color: color,
                        },
                    }

                });
            }
        }
    }
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
    var date;
    if (p == 0) 
        date = document.getElementById('dateInput').value;

    var pageIndexSpan = document.getElementById('PageIndex');
    _pageIndex = _pageIndex + p;
    pageIndexSpan.innerHTML = _pageIndex;
    $.post("/PriceView/ListView", { Type: _type, Date: date, pageIndex: _pageIndex })
        .done(function (data) {
            _obj = data.Data;
            _obj = _obj.reverse();
            _sessions = _obj.filter((item) => item.Session > 0);
            _biggerThanSD = _obj.filter((item) => item.BiggerThanSD);
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

        name: 'SD',
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


myChart.on('click', params => {
    if (params.seriesName === "SD" && params.seriesType == "scatter") {
        var d = params.data;

        var f = _time.findIndex((item) => item == d[0]);

        var max = _obj.find((item) => item.ID == _obj[f].BiggerThanSD.MaxPriceID);
        var min = _obj.find((item) => item.ID == _obj[f].BiggerThanSD.MinPriceID);
        alert(`max: ${getTimesByID(max.ID)} ${max.Close}  |  min: ${getTimesByID(min.ID)} ${min.Close}  `);
    }
})