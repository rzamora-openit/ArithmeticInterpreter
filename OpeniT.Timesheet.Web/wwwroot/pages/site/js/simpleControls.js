// simpleControls.js

(function() {
	"use strict";

	var app = angular.module("simpleControls", []);

	app.directive("elapsedTime", function() {
		return {
			templateUrl: "/pages/site/views/elapsedTime.html"
		}
	});

	app.directive("autoUncollapseMobile", function () {
		return {
			require: 'uib-accordion',
			link: function (scope, element, attr, uibAccordion) {
				scope.$watch(function () { return uibAccordion.groups.length; }, function () {
					angular.forEach(uibAccordion.groups, function (group) {
						group.isOpen = window.innerWidth >= 414;
					});
				});
			}
		}
	});

	app.directive('smartTableCurrentPage', function () {
		return {
			require: '^stTable',
			templateUrl: '/pages/site/views/smartTableCurrentPage.html',
			link: function (scope, element, attr, stTable) {
				scope.$watch(function () {
					return stTable.tableState();
				}, function () {
					var tableState = stTable.tableState();
					if (tableState) {
						scope.pagination = tableState.pagination;
					}
				}, true);
			}
		}
	});

	app.directive("limitInput", function () {
		return {
			require: 'ngModel',
			restrict: "A",
			link: function (scope, element, attrs, ngModelCtrl) {
				var el = element[0];
				el.style.maxWidth = (((attrs.max.toString().length + 1) * 7.5) + 50) + 'px';
				el.addEventListener("focus", function (e) {
					this.select();
				});

				scope.$watch(function () {
					return ngModelCtrl.$viewValue;
				}, function () {
					if (attrs.min && attrs.max) {
						if (!ngModelCtrl.$viewValue || parseInt(attrs.min) > parseInt(ngModelCtrl.$viewValue)) {
							ngModelCtrl.$setViewValue(attrs.min);
							ngModelCtrl.$render();
						}
						if (parseInt(attrs.max) < parseInt(ngModelCtrl.$viewValue)) {
							ngModelCtrl.$setViewValue(attrs.max);
							ngModelCtrl.$render();
						}
					}

					el.style.width = ((ngModelCtrl.$viewValue.toString().length * 7.5) + 50) + 'px';
				}, true);
			}
		}
	});

	app.directive("backButton", function($window) {
		return {
			restrict: "A",
			scope: {
				fallback: "@"
			},
			link: function (scope, element, attrs) {
				element.bind("click", function () {
					var isTimesheetUrl = $window.document.referrer && new URL($window.location.href).origin === new URL($window.document.referrer).origin;
					var samePrefix = !scope.prefix || !new URL($window.location.href).hash || new URL($window.location.href).hash.startsWith(scope.prefix);
					if ($window.history.length > 1 && isTimesheetUrl && samePrefix) {
						$window.history.back();
					} else {
						$window.location.href = scope.fallback || "#!/";
					}

					scope.$apply();
				});
			}
		}
	});

	app.directive("stResetSearch", function() {
		return {
			restrict: "A",
			require: "^stTable",
			link: function(scope, element, attr, ctrl) {
				return element.bind("click", function () {
					return scope.$apply(function () {
						var tableState = ctrl.tableState();
						tableState.search.predicateObject = {};
						tableState.pagination.start = 0;
						return ctrl.pipe();
					});
				});
			}
		};
	});

	app.directive("autoLinker", ["$timeout", "$compile", "$filter",
		function ($timeout, $compile, $filter) {
			return {
				restrict: "A",
				scope: {
					autoLinkerTags: "=",
					autoLinkerHashtag: "@"
				},
				link: function(scope, element, attr) {
					$timeout(function() {
						var html = element.html();
						var uriTag = "#";

						if (html === "") {
							return false;
						}

						if (attr.autoLinkerHashtag) {
							uriTag = attr.autoLinkerHashtag;
						}

						// Let's start with hashtag
						var foundItem = $filter("filter")(scope.autoLinkerTags, { prefix: uriTag })[0];
						html = html.replace(/(^|\s)*(#)(\d+)/g, '$1<a href="' + foundItem.uri + '$3" target="blank">$2$3</a>');

						// Then iterate all tags and replace their prefixes accordingly
						angular.forEach(scope.autoLinkerTags, function(value) {
							var regex = new RegExp("(^|\s)*(\\b" + value.prefix + ")(\\d+)", "g");
							html = html.replace(regex, '$1<a href="' + $filter("filter")(scope.autoLinkerTags, { prefix: value.prefix }, true)[0].uri + '$3" target="blank">$2$3</a>');
						});

						element.html(html);
						return $compile(element.contents())(scope);
					}, 0);
				}
			};
		}
    ]);

    app.factory('gradientProvider', function () {
        var service = {};

        service.ColorStop = function (stop, rgba) {
            this.stop = stop;
            this.rgba = rgba;
        };

        service.ColorGradient = function (colorStops) {
            this.colorStops = sortColorStops(colorStops);
            this.getRed = function (stop) { return getGradient(stop, 0); };
            this.getGreen = function (stop) { return getGradient(stop, 1); };
            this.getBlue = function (stop) { return getGradient(stop, 2); };
            this.getAlpha = function (stop) { return getGradient(stop, 3); };
            this.getRGB = function (stop) { return [getGradient(stop, 0), getGradient(stop, 1), getGradient(stop, 2)]; };
            this.getRGBA = function (stop) { return [getGradient(stop, 0), getGradient(stop, 1), getGradient(stop, 2), getGradient(stop, 3)]; };
            this.getStringRGB = function (stop) { return getGradient(stop, 0) + "," + getGradient(stop, 1) + "," + getGradient(stop, 2); };
            this.getStringRGBA = function (stop) { return getGradient(stop, 0) + "," + getGradient(stop, 1) + "," + getGradient(stop, 2) + "," + getGradient(stop, 3); };
            this.setTransitionRate = function (transitionRate) {
                if (transitionRate > 1) {
                    my.transitionRate = transitionRate;
                }
                return this;
            };
            //ShortHands
            this.getR = this.getRed;
            this.getG = this.getGreen;
            this.getB = this.getBlue;
            this.getA = this.getAlpha;

            //Local Access Variables
            var my = {};
            my.colorStops = colorStops;

            //Local Functions
            function getGradient(stop, colorIndex) {
                var stopIndex = getContainingStopIndex(stop);
                if (stopIndex === -1) {
                    return my.colorStops[0].rgba[colorIndex];
                } else if (stopIndex === my.colorStops.length) {
                    return my.colorStops[my.colorStops.length - 1].rgba[colorIndex];
                } else {
                    //Interpolation Formula							
                    var x1 = my.colorStops[stopIndex].stop;
                    var x2 = my.colorStops[stopIndex + 1].stop;
                    var y1 = my.colorStops[stopIndex].rgba[colorIndex];
                    var y2 = my.colorStops[stopIndex + 1].rgba[colorIndex];
                    var x_ = stop;
                    if (my.transitionRate) {
                        x_ = Math.floor(x_ / my.transitionRate) * my.transitionRate;
                    }
                    var y_ = ((x_ - x1) / (x2 - x1) * (y2 - y1)) + y1;
                    return Math.floor(y_);
                }
            }
            function getContainingStopIndex(stop) {
                var l = my.colorStops.length;
                if (stop < my.colorStops[0].stop) {
                    return -1;
                } else if (stop >= my.colorStops[l - 1].stop) {
                    return l;
                } else {
                    for (var i = 0; i < l - 1; i++) {
                        if (
                            my.colorStops[i].stop <= stop &&
                            stop < my.colorStops[i + 1].stop
                        ) {
                            return i;
                        }
                    }
                }
            }
            function isColorStopsSorted(colorStops) {
                var l = colorStops.length;
                var isSorted = true;
                for (var i = 1; i < l; i++) {
                    if (colorStops[i - 1].stop > colorStops[i].stop) {
                        isSorted = false;
                        break;
                    }
                }
                return isSorted;
            }
            function sortColorStops(colorStops) {
                if (isColorStopsSorted(colorStops)) {
                    return colorStops;
                } else {
                    return colorStops.sort(function (colorStopA, colorStopB) {
                        return colorStopA.stop - colorStopB.stop;
                    });
                }
            }
        };	

        return service;
    });

    app.factory('durationProvider', function () {
        var service = {};

        service.GetHoursPassed = function (dateStr) {return hoursPassed(dateStr);};

        service.GetDaysPassed = function (dateStr) { return daysPassed(dateStr); };

        service.GetWeeksPassed = function (dateStr) { return weeksPassed(dateStr); };

        service.GetMonthsPassed = function (dateStr) { return monthsPassed(dateStr); };

        service.GetYearPassed = function (dateStr) { return yearPassed(dateStr); };

        service.getDurationString = function (dateStr) { return durationString(dateStr); };

        service.getDurationCompleteString = function (dateStr) { return durationCompleteString(dateStr); };

		service.getDateDifference = function (dateStr) { return dateDifference(dateStr); };

		service.getDayDifference = function (dateStr) { return dayDifference(dateStr); };

		function durationString(dateStr) {
			if (daysPassed(dateStr) < 2) {
				if (dateDifference(dateStr) === 0) {
					return "today";
				} else if (dateDifference(dateStr) === 1) {
					return "yesterday";
				} else {
					return "2 days";
				}
			}
			else if (daysPassed(dateStr) < 8) {
				return daysPassed(dateStr) + " days";
			}
			else if (weeksPassed(dateStr) < 5) {
				if (weeksPassed(dateStr) === 1) {
					return weeksPassed(dateStr) + " week";
				} else {
					return weeksPassed(dateStr) + " weeks";
				}
			}
			else if (monthsPassed(dateStr) < 13) {
				if (monthsPassed(dateStr) === 1 || monthsPassed(dateStr) === 0) {
					return 1 + " month";
				} else {
					return monthsPassed(dateStr) + " months";
				}
			}
			else {
				var years = Math.floor(monthsPassed(dateStr) / 12);
				if (years === 1) {
					return years + " year";
				} else {
					return years + " years";
				}
			}
		}

		function durationCompleteString(dateStr) {
			if (daysPassed(dateStr) < 2) {
				if (dateDifference(dateStr) === 0) {
					return "today.";
				} else if (dateDifference(dateStr) === 1) {
					return "yesterday.";
				} else {
					return "2 days ago.";
				}
			}
			else if (daysPassed(dateStr) < 8) {
				return daysPassed(dateStr) + " days ago.";
			}
			else if (weeksPassed(dateStr) < 5) {
				if (weeksPassed(dateStr) === 1) {
					return weeksPassed(dateStr) + " week ago.";
				} else {
					return weeksPassed(dateStr) + " weeks ago.";
				}
			}
			else if (monthsPassed(dateStr) < 13) {
				if (monthsPassed(dateStr) === 1 || monthsPassed(dateStr) === 0) {
					return 1 + " month ago.";
				} else {
					return monthsPassed(dateStr) + " months ago.";
				}
			}
			else {
				var years = Math.floor(monthsPassed(dateStr) / 12);
				if (years === 1) {
					return years + " year ago.";
				} else {
					return years + " years ago.";
				}
			}
		}

        function dateDifference(dateStr) {
            var dateWhen = new Date(dateStr);
            var dateNow = new Date();

            return Math.abs(dateNow.getDate() - dateWhen.getDate());
		}

		function dayDifference(dateStr) {
			var dateWhen = new Date(dateStr);
			var dateNow = new Date();
			var date1 = new Date(dateWhen.getFullYear() + '-' + (dateWhen.getMonth() + 1) + '-' + dateWhen.getDate());
			var date2 = new Date(dateNow.getFullYear() + '-' + (dateNow.getMonth() + 1) + '-' + dateNow.getDate());
			var timeDiff = date1.getTime() - date2.getTime();
			var daysPassed = Math.round(timeDiff / (1000 * 3600 * 24), 0);
			return daysPassed;
		}

        function hoursPassed (dateStr) {
            var dateWhen = new Date(dateStr);
            var dateNow = new Date();
            var timeDiff = Math.abs(dateNow.getTime() - dateWhen.getTime());
            var HoursPassed = Math.floor(timeDiff / (1000 * 3600));
            return HoursPassed;
        }
        function daysPassed (dateStr) {
            var dateWhen = new Date(dateStr);
            var dateNow = new Date();
            var timeDiff = Math.abs(dateNow.getTime() - dateWhen.getTime());
            var daysPassed = Math.floor(timeDiff / (1000 * 3600 * 24));
            return daysPassed;
        }
        function weeksPassed (dateStr) {
            var dateWhen = new Date(dateStr);
            var dateNow = new Date();
            var timeDiff = Math.abs(dateNow.getTime() - dateWhen.getTime());
            var weeksPassed = Math.floor(timeDiff / (1000 * 3600 * 24 * 7));
            return weeksPassed;
        }
        function monthsPassed (dateStr) {
            var dateWhen = new Date(dateStr);
            var dateNow = new Date();

            var yearWhen = dateWhen.getFullYear();
            var yearNow = dateNow.getFullYear();

            if (yearWhen === yearNow) {
                return Math.abs(dateNow.getMonth() - dateWhen.getMonth());
            } else {
                return ((yearNow - yearWhen - 1) * 12) + (11 - dateWhen.getMonth()) + dateNow.getMonth();
            }
        }
        function yearPassed (dateStr) {
            var dateWhen = new Date(dateStr);
            var dateNow = new Date();

            var yearWhen = dateWhen.getFullYear();
            var yearNow = dateNow.getFullYear();

            return yearNow - yearWhen;
        }

        function CSticksToJSDate(ticks) {
            //https://stackoverflow.com/questions/15486299/convert-c-sharp-net-datetime-ticks-to-days-hours-mins-in-javascript

            //ticks are in nanotime; convert to microtime
            var ticksToMicrotime = ticks / 10000;

            //ticks are recorded from 1/1/1; get microtime difference from 1/1/1/ to 1/1/1970
            var epochMicrotimeDiff = Math.abs(new Date(0, 0, 1).setFullYear(1));

            //new date is ticks, converted to microtime, minus difference from epoch microtime
            var tickDate = new Date(ticksToMicrotime - epochMicrotimeDiff);
            return tickDate;
        }


        return service;
	});

	app.filter("utcDate", function () {
		return function (date) {

			var prevDate = new Date(date);
			var newDate = new Date(prevDate.getTime() + prevDate.getTimezoneOffset() * 60 * 1000);

			var offset = prevDate.getTimezoneOffset() / 60;
			var hours = prevDate.getHours();

			newDate.setHours(hours - offset);

			return newDate;   
		};
	});

	app.filter("filterUser", function () {
		return function (users, search) {
			if (users === undefined) {
				return [];
			} else if (search === undefined || search === '') {
				return users;
			} else {
				var temp = [];
				if (users) {
					users.forEach(function (user) {
						if (user.displayName.toLowerCase().indexOf(search.toLowerCase()) > -1 || user.email.toLowerCase().indexOf(search.toLowerCase()) > -1) {
							temp.push(user);
						}
					});
				}
				return temp;
			}
		};
	});

	app.filter("convertSiteValuesArrayToModel", function () {
		return function (siteValuesArray) {
			var model = {};
			for (var i = 0; i < siteValuesArray.length; i++) {
				var siteValue = siteValuesArray[i];
				model[siteValue.key] = siteValue.value;
			}

			return model;
		};
	});

	app.filter("convertModelToSiteValuesArray", function () {
		return function (model) {
			var siteValuesArray = [];
			for (var key in model) {
				var siteValue = {};
				siteValue.key = key;
				siteValue.value = model[key];

				siteValuesArray.push(siteValue);
			}

			return siteValuesArray;
		};
	});

	app.filter("stringArrayToQueryString", function () {
		return function (queryStringName, stringArray) {
			var queryString = "?";
			for (var i = 0; i < stringArray.length; i++) {
				if (i != stringArray.length - 1) {
					queryString += queryStringName + "=" + stringArray[i] + "&";
				} else {
					queryString += queryStringName + "=" + stringArray[i];
				}
			}

			return queryString;
		}
	});

	app.filter("limitItem", function () {
		return function (arr, limit) {		
			if (limit === null || limit === "") {
				return arr;
			}
			if (arr.length > limit) {
				return arr.slice(0, limit);
			} else {
				return arr;
			}
		};
	});

	app.filter('findClosestScrollContainer', function ($filter) {
		return function (element) {
			if (!element) {
				return null;
			}

			var style = getComputedStyle(element);
			if (style.overflowY !== 'visible' && element.style.overflowY !== 'visible') {
				return element;
			}

			return $filter("findClosestScrollContainer")(element.parentElement);
		}
	});

	app.filter('scrollTo', function () {
		return function (wdw, elm, margin, speed) {
			wdw = wdw || window;
			elm = elm || window.document.documentElement;
			margin = margin || 10;
			speed = speed || 10;

			// This scrolling function 
			// is from http://www.itnewb.com/tutorial/Creating-the-Smooth-Scroll-Effect-with-JavaScript

			var startY = currentYPosition();
			var stopY = elmYPosition() - margin;
			var distance = stopY > startY ? stopY - startY : startY - stopY;
			if (distance < 100) {
				wdw.scrollTo(0, stopY); return;
			}
			var step = Math.round(distance / 25);
			var leapY = stopY > startY ? startY + step : startY - step;
			var timer = 0;
			if (stopY > startY) {
				for (var i = startY; i < stopY; i += step) {
					setTimeout(function () {
						wdw.scrollTo(0, leapY);
					}, timer * speed);
					leapY += step; if (leapY > stopY) leapY = stopY; timer++;
				} return;
			}
			for (var i = startY; i > stopY; i -= step) {
				setTimeout(function () {
					wdw.scrollTo(0, leapY);
				}, timer * speed);
				leapY -= step; if (leapY < stopY) leapY = stopY; timer++;
			}

			function currentYPosition() {
				// Firefox, Chrome, Opera, Safari
				if (self.pageYOffset) return self.pageYOffset;
				// Internet Explorer 6 - standards mode
				if (document.documentElement && document.documentElement.scrollTop)
					return document.documentElement.scrollTop;
				// Internet Explorer 6, 7 and 8
				if (document.body.scrollTop) return document.body.scrollTop;
				return 0;
			}

			function elmYPosition() {
				if (elm) {
					var y = elm.offsetTop;
					var node = elm;
					while (node.offsetParent && node.offsetParent != document.body) {
						node = node.offsetParent;
						y += node.offsetTop;
					} return y;
				}
			}
		}
	});

	app.filter('isElementInViewport', function () {
		return function (wdw, elm) {
			var rect = elm.getBoundingClientRect();

			return (
				rect.top >= 0 &&
				rect.left >= 0 &&
				rect.bottom <= (window.innerHeight || wdw.clientHeight) &&
				rect.right <= (window.innerWidth || wdw.clientWidth)
			);
		}
	});

	app.directive('scrollOnClick', function ($filter) {
		return {
			restrict: 'A',
			link: function (scope, element, attrs) {
				element.on('click', function () {
					if (attrs.delay) {
						window.setTimeout(scroll, parseInt(attrs.delay));
					} else {
						scroll();
					}
				});

				var scroll = function () {
					var idToScroll = attrs.idToScroll;
					if (idToScroll) {
						$filter('scrollTo')(window, document.getElementById(idToScroll), (angular.element("#topnav").height() + 10), 10);
					}
				};
			}
		}
	});

	app.filter('trustAsHtml', ['$sce', function ($sce) {
		return function (text) {
			return $sce.trustAsHtml(text);
		};
	}]);
})();