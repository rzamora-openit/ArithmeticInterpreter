// site.js

function stickyFunction() {
	//if IE
	var userAgent = window.navigator.userAgent;
	var msie = userAgent.indexOf("MSIE ");	
	if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
		document.getElementsByTagName("BODY")[0].style.paddingTop = "50px";		
	}
	else {
		document.getElementById("topnav").style.position = "sticky";
	}
};
window.onload = stickyFunction();