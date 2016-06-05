var elem = $('ul.content-slider.items');

$.getJSON('./model.json', function(data) {
	console.log(data);

	for(var i = 0;i < data.activitys.length;i++) {
		var node = document.createElement('li');
		node.setAttribute('class', 'item thumb investigations');
		node.innerHTML = '<figure><div class="investigations"><h5>' + data.activitys[i].name + '</h5><p>Einstelldatum: ' + data.activitys[i].insertion_date + '</p><p>Crowd-Anteil: ' + data.activitys[i].total_crowd_investment / data.activitys[i].investment_goal + ' %</p><p>Crowd-Grenze: ' + data.activitys[i].investment_goal + ' €</p><p>Crowd-total: ' + data.activitys[i].total_crowd_investment + ' €</p><p><a href="#">Abbrechen</a></p><p><a href="#">Ändern</a></p></div></figure>';
		elem['0'].appendChild(node);	
	}
});
