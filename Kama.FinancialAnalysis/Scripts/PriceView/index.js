
var obj;
var baseLabels;
var pageIndex = 1;
var chart;
var chart2;

var isNykOpen ;
var isNykClose;
var isLndOpen ;
var isLndClose ;

var isMovingAverages  = true;

getData(0);


function getData(p) {
	pageIndex = pageIndex + p;
	$("#PageIndex").text(pageIndex - 1);
	scrollDiv('chrtCenter')
	$.post("PriceView/ListView", { Type: 1, pageIndex: pageIndex })
		.done(function (data) {
			obj = data.Data;
			baseLabels = obj.Bases.map(x => new Date(x.Date.match(/\d+/)[0] * 1));

			destroyChart();

		})
		.fail(function (e) {
			alert("error" + e);
		});
}
function destroyChart() {
	if (chart) {
		chart.destroy();
		chart2.destroy();
	}
	InitEurUsd();
}

function scrollDiv(idName) {
	const scrollContainer = document.getElementById(idName);
	scrollContainer.scrollTo({
		top: 0,
		left: scrollContainer.scrollWidth,
		behavior: 'smooth'
	});
};

function InitEurUsd() {

	var ctx = document.getElementById('chartViwe').getContext('2d');
	ctx.canvas.width = 30000;
	ctx.canvas.height = 800;
	var workingHours = [];
	var barData = getRandomData();
	chart2Init();

	function WorkingHours(r) {
		var color;
		borderWidth = 2;
		switch (r) {
			case 'nykOpen':
				color = '#3dff00'
				if (!isNykOpen) borderWidth = 0;
				break;
			case 'nykClose':
				color = '#00ffde'
				if (!isNykClose) borderWidth = 0;
				break;
			case 'lndOpen':
				color = '#0006ff'
				if (!isLndOpen) borderWidth = 0;
				break;
			case 'lndClose':
				if (!isLndClose) borderWidth = 0;
			default:
				color = '#ff0000'
				break;
		}

		return {
			type: 'line',
			label: 'w h ' + r,
			borderColor: color,
			backgroundColor: 'transparent',
			borderWidth: borderWidth,
			data: data(),
		}

		function data() {
			return workingHours.map(d => {
				return {
					x: d.x,
					y: d[r]
				}
			})
		};
	};
	function MovingAverages(r) {

		borderWidth = 1;
		if (!isMovingAverages) borderWidth = 0;
		var color;
		switch (r) {
			case 'M5':
				color = '#ffe200'
				break;
			case 'M10':
				color = '#3dff00'
				break;
			case 'M20':
				color = '#00ffde'
				break;
			case 'M30':
				color = '#0006ff'
				break;
			case 'H1':
			default:
				color = '#ff0000'
				break;
		}

		return {
			type: 'line',
			label: 'm a ' + r,
			borderColor: color,
			backgroundColor: 'transparent',
			borderWidth: borderWidth,
			data: data(),
		}

		function data() {
			return obj.MovingAverages.map(d => {
				return {
					x: (d.Date.match(/\d+/)[0] * 1),
					y: d[r]
				}
			})
		};
	};

	chart = new Chart(ctx, {
		type: 'candlestick',
		data: {
			datasets: [
				{
					label: 'CHRT',
					data: barData,
					order: 0
				},
				MovingAverages('M5'),
				MovingAverages('M10'),
				MovingAverages('M20'),
				MovingAverages('M30'),
				MovingAverages('H1'),
				WorkingHours('nykOpen'),
				WorkingHours('nykClose'),
				WorkingHours('lndOpen'),
				WorkingHours('lndClose'),
			]
		},
		options: {
			elements: {
				point: {
					radius: 0
				}
			},
			plugins: {
				legend: {
					display: false
				}
			}
		}
	});

	function getRandomData() {
		//var date = luxon.DateTime.fromRFC2822(dateStr);

		//var data = [randomBar(date, 30)];
		//while (data.length < count) {
		//	date = date.plus({days: 1});
		//	if (date.weekday <= 5) {
		//		data.push(randomBar(date, data[data.length - 1].c));
		//	}
		//}
		data = [];
		obj.Bases.map((x) => {
			workingHours.push({
				x: x.Date.match(/\d+/)[0] * 1,
				nykOpen: obj.WorkingHour.NykOpen,
				nykClose: obj.WorkingHour.NykClose,
				lndOpen: obj.WorkingHour.LndOpen,
				lndClose: obj.WorkingHour.LndClose,
			});
			data.push({
				x: x.Date.match(/\d+/)[0] * 1,
				o: x.Open,
				h: x.Max,
				l: x.Min,
				c: x.Close
			});
		});
		return data;
	}

	var update = function () {
		//var dataset = chart.config.data.datasets[0];

		//// candlestick vs ohlc
		//var type = document.getElementById('type').value;
		//dataset.type = type;

		//// linear vs log
		//var scaleType = document.getElementById('scale-type').value;
		//chart.config.options.scales.y.type = scaleType;

		//// color
		//var colorScheme = document.getElementById('color-scheme').value;
		//if (colorScheme === 'neon') {
		//	dataset.color = {
		//		up: '#01ff01',
		//		down: '#fe0000',
		//		unchanged: '#999',
		//	};
		//} else {
		//	delete dataset.color;
		//}

		//// border
		//var border = document.getElementById('border').value;
		//var defaultOpts = Chart.defaults.elements[type];
		//if (border === 'true') {
		//	dataset.borderColor = defaultOpts.borderColor;
		//} else {
		//	dataset.borderColor = {
		//		up: defaultOpts.color.up,
		//		down: defaultOpts.color.down,
		//		unchanged: defaultOpts.color.up
		//	};
		//}

		//// mixed charts
		//var mixed = document.getElementById('mixed').value;
		//if(mixed === 'true') {
		//	chart.config.data.datasets = [
		//		{
		//			label: 'CHRT - Chart.js Corporation',
		//			data: barData
		//		},
		//		{
		//			label: 'Close price',
		//			type: 'line',
		//			data: lineData()
		//		}	
		//	]
		//}
		//else {
		//	chart.config.data.datasets = [
		//		{
		//			label: 'CHRT - Chart.js Corporation',
		//			data: barData
		//		}	
		//	]
		//}

		chart.update();
	};

	function chart2Init() {
		const ctx2 = document.getElementById('chartViwe2').getContext('2d');
		ctx2.canvas.width = 30000;
		ctx2.canvas.height = 300;
		var t = standardDeviation('D1')

		var barData2 = []
		obj.Bases.map((x) => {
			barData2.push({
				x: x.Date.match(/\d+/)[0] * 1,
				o: 0,
				h: 0,
				l: 0,
				c: 0
			});
		});

		chart2 = new Chart(ctx2, {
			type: 'candlestick',
			data: {
				datasets: [
					{
						label: 'CHRT',
						data: barData2,
						order: 0
					},
					standardDeviation('M10'),
					standardDeviation('M30'),
					standardDeviation('H1'),
					standardDeviation('H12'),
					standardDeviation('D1'),
				]
			},
			options: {
				elements: {
					point: {
						radius: 0
					}
				},
				plugins: {
					legend: {
						display: false
					}
				}
			}
		});

		function standardDeviation(r) {

			var color;
			switch (r) {
				case 'M10':
					color = '#ffe200'
					break;
				case 'M30':
					color = '#3dff00'
					break;
				case 'H1':
					color = '#00ffde'
					break;
				case 'H12':
					color = '#0006ff'
					break;
				case 'D1':
				default:
					color = '#ff0000'
					break;
			}

			return {
				type: 'line',
				label: r,
				borderColor: color,
				backgroundColor: 'transparent',
				borderWidth: 1,
				data: data(),
			}

			function data() {
				return obj.StandardDeviations.map(d => {
					return {
						x: (d.Date.match(/\d+/)[0] * 1),
						y: d[r]
					}
				})
			};
		};

	}
};


//document.getElementById('update').addEventListener('click', update);

//document.getElementById('randomizeData').addEventListener('click', function() {
//	barData = getRandomData(initialDateStr, barCount);
//	update();
//});


//var obj = @Html.Raw(
//	Json.Encode((Kama.FinancialAnalysis.Model.PriceView)ViewBag.List)
//);
//var baseAsc = obj.Bases.filter((x) => x.Asc);
//var baseDes = obj.Bases.filter((x) => !x.Asc);
//var baseLabels = obj.Bases.map(x => x.Date);


//const ctx1 = document.getElementById('myChart');
//function getRandomInt(min, max) {
//	min = Math.ceil(min);
//	max = Math.floor(max);
//	return Math.floor(Math.random() * (max - min) + min);
//}

//const labels1 = ['Red', 'Blue', 'Green', 'Purple', 'Orange', 'ss'];
//const labels = ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange', 'ss'];
//const data = {
//	labels: labels,
//	datasets: [
//		{
//			label: 'نزولی',
//			data: labels.map(() => {
//				return [getRandomInt(-100, 100), getRandomInt(-100, 100)];
//			}),
//			backgroundColor: 'Red',
//		},
//		{
//			label: 'صعودی',
//			data: labels1.map(() => {
//				return [getRandomInt(-100, 100), getRandomInt(-100, 100)];
//			}),
//			backgroundColor: 'Blue',
//		},
//	]
//};

//new Chart(ctx1, {
//	type: 'bar',
//	data: data,
//	options: {
//		responsive: true,
//		plugins: {
//			legend: {
//				position: 'top',
//			},
//			title: {
//				display: true,
//			}
//		}
//	}
//});