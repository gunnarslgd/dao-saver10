var onPageLoaded = () => {
	console.log("onPageLoaded");
	//loadMarketChart("NASDAQ100");
}

var loadMarketChart = (key) => {
//	var container = document.getElementById("chart-container");
//	var chart = document.createElement("div");
//	container.append(chart);

	const labels = [
		'January',
		'February',
		'March',
		'April',
		'May',
		'June',
	];

	const data = {
		labels: labels,
		datasets: [{
			label: 'My First dataset',
			backgroundColor: 'rgb(255, 99, 132)',
			borderColor: 'rgb(255, 99, 132)',
			data: [0, 10, 5, 2, 20, 30, 45],
		}]
	};

	const config = {
		type: 'line',
		data: data,
		options: {}
	};

	const myChart = new Chart(
		document.createElement("NASDAQ100"),
		config
	);

//	fetch(`MarketData?key=${key}`)
//		.then(response => response.json())
//	.then(data => {
// 	});
}