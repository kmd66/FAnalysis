
var baseLabels;
var pageIndex = 1;
var chart;

getData(0, 'xauusd', 2);
getData(0, 'usdchf', 3);
getData(0, 'eurjpy', 4);


function getData(p,symbol, type) {
	pageIndex = pageIndex + p;
	//$("#PageIndex").text(pageIndex - 1);
	//scrollDiv('chrtCenter')
	$.post("/PriceView/ListView", { Type: type, pageIndex: pageIndex })
		.done(function (data) {
			debugger
			chart2Init(symbol, data.Data)
		})
		.fail(function (e) {
			alert("error" + e);
		});
}


function scrollDiv(idName) {
	const scrollContainer = document.getElementById(idName);
	scrollContainer.scrollTo({
		top: 0,
		left: scrollContainer.scrollWidth,
		behavior: 'smooth'
	});
};


function chart2Init(chartIdName, obj) {
	const ctx2 = document.getElementById(chartIdName).getContext('2d');
	ctx2.canvas.width = 30000;
	ctx2.canvas.height = 300;

	var barData2 = []
	obj.Bases.map((x) => {
		barData2.push({
			x: obj.Bases[0].Date.match(/\d+/)[0] * 1,
			o: obj.Bases[0].Close,
			h: obj.Bases[0].Close,
			l: obj.Bases[0].Close,
			c: obj.Bases[0].Close
		});
	});

	new Chart(ctx2, {
		type: 'candlestick',
		data: {
			datasets: [
				{
					label: 'CHRT',
					data: barData2,
					order: 0
				},
				standardDeviation()
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

	function standardDeviation() {


		return {
			type: 'line',
			label: 'line',
			borderColor: '#0006ff',
			backgroundColor: 'transparent',
			borderWidth: 1,
			data: data(),
		}

		function data() {
			return obj.Bases.map(d => {
				return {
					x: (d.Date.match(/\d+/)[0] * 1),
					y: d.Close
				}
			})
		};
	};

}


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