//var module = angular.module('uccm.app.wf.detail');

app.controller("wfDetailCtrl", function ($scope, $xhttp, $modal, $location) {
    $scope.id = $location.search().id;
    $scope.sourceNodes = [];
    $scope.sourceEdges = [];

    $scope.init = function () {
        if ($scope.id) {
            // edit mode
            $scope.loadWfDetail($scope.id)
            
        }
    };

    // get detail metadata of current Workflow
    $scope.loadWfDetail = function (id) {
        $xhttp.get('/api/WF/GetWorkflowDetail?wfId=' + id).then(function (response) {
            $scope.wfProcess = response.data;
            $scope.sourceNodes = [];
            $scope.sourceEdges = [];
            $scope.wfProcess.Steps.forEach(function (step) {
                $scope.sourceNodes.push({
                    id: step.Id,
                    label: step.Name,
                    shape: 'box'
                })
            });

            $scope.wfProcess.Routes.forEach(function (route) {
                $scope.sourceEdges.push({
                    from: route.FromStepId,
                    to: route.ToStepId,
                    arrows: 'to'
                })
            });

            $scope.bindData();
        });
    }

    $scope.createWf = function () {
        if ($scope.wfProcess) {
            $xhttp.post('/api/WF/CreateWFProcess', $scope.wfProcess).then(function (response) {
                $scope.wfProcess = response.data;
                $scope.id = $scope.wfProcess.Id;
            });
        }
    }

    $scope.updateWf = function () {
        if ($scope.wfProcess) {
            $xhttp.post('/api/WF/UpdateWFProcess', $scope.wfProcess).then(function (response) {
            });
        }
    }

    $scope.createRoute = function (from, to) {
        var newRoute = { FromStepId: from, ToStepId: to };
        $xhttp.post('/api/WF/CreateWFRoute', newRoute).then(function (response) {
            console.log("Create new route: ");
            console.dir(response.data);
        });
    }

    // create a new step for current workflow
    $scope.createStep = function () {
        //var deferrer = $q.defer();

        if ($scope.newStep) {
            $scope.newStep.WorkflowId = $scope.id;
            // submit to server
            $xhttp.post('/api/WF/CreateWFStep', $scope.newStep).then(function (response) {
                var recentStep = response.data;
                // add new
                var recentId = recentStep.Id;
                $scope.sourceNodes.push({ id: recentId, label: $scope.newStep.Name, shape: 'box' });
                /*if ($scope.selectedNode) {
                    $scope.sourceEdges.push({ from: $scope.selectedNode.id, to: recentId, arrows: 'to' })
                }*/
                $scope.bindData();
                $scope.newStep = {};
                //deferrer.resolve(response.data);
            });
        }
        //deferrer.resolve(null);
        //return deferrer.promise;
    }
    var network;

    // bind data to diagram
    $scope.bindData = function(){
        var nodes = new vis.DataSet($scope.sourceNodes);
        var edges = new vis.DataSet($scope.sourceEdges);

        // create a network
        var container = document.getElementById('screen');

        // provide the data in the vis format
        var data = {
            nodes: nodes,
            edges: edges
        };
        var options = {
            manipulation: {
                addNode: function (data, callback) {
                    // filling in the popup DOM elements
                    alert("add node done");
                },
                editNode: function (data, callback) {
                    // filling in the popup DOM elements
                    alert("edit node done");
                },
                addEdge: function (data, callback) {
                    data.arrows = 'to';
                    if (data.from == data.to) {
                        var r = confirm("Do you want to connect the node to itself?");
                        if (r == true) {
                            callback(data);
                        }
                    }
                    else {
                        callback(data);
                        $scope.createRoute(data.from, data.to);
                    }
                }
            }
        };

        // initialize your network!
        if (!network) {
            network = new vis.Network(container, data, options);
            network.on("click", function (params) {
                //params.event = "[original event]";
                var id = params.nodes[0];
                for (var i = 0; i < sourceNodes.length; i++) {
                    if (sourceNodes[i].id == id) {
                        $scope.selectedNode = sourceNodes[i];
                        break;
                    }
                }
                
                
            });
        }
        else {
            network.setData(data);
        }
        network.enableEditMode();
        //network.addEdgeMode();
    }

    /*$scope.createStep = function () {
        if ($scope.newStep) {
            // submit to server
            $scope.createStep()

            // add new
            var recentId = sourceNodes.length + 1;
            sourceNodes.push({ id: recentId, label: $scope.newStep.Name, shape: 'box' });

            if ($scope.selectedNode) {
                sourceEdges.push({ from: $scope.selectedNode.id, to: recentId, arrows: 'to' })
            }
            $scope.bindData();
            $scope.newStep = {};
        }
    }*/

    /*************** CREATE NEW NODE **********************************/
    var drag = false;
    var passed = false;
    var screen = $("#screen");
    var preview = $(".preview");
    $scope.mousedown = function () {
        if ($scope.newStep && $scope.newStep.Name.trim() != '') {
            var width = preview.outerWidth();
            preview.addClass("drag");
            preview.width(width);
            drag = true;
            passed = false;
        }
    };
    $scope.mouseup = function () {
        if (drag) {
            preview.removeClass("drag");
            preview.removeAttr("style");
            drag = false;

            if (passed) {
                $scope.createStep();
                passed = false;
                screen.removeAttr("dropable");
            }
        }
    };
    $scope.mouseover = function () {
        if (drag) {
            screen.addClass("dropable");
            passed = true;
        }
    };
    $scope.mouseleave = function () {
        if (drag) {
            screen.removeClass("dropable");
            passed = false;
        }
    };
    $scope.mousemove = function ($event) {
        if (drag) {
            // remove highlight
            if (window.getSelection) {
                var selection = window.getSelection();
                if (selection.rangeCount > 0) {
                    window.getSelection().removeAllRanges();
                }
            }

            var width = preview.outerWidth();
            var height = preview.outerHeight();
            var left = $event.pageX - width / 2;
            var top = $event.pageY - height / 2;
            preview.css({ "left": left, "top": top });

            var scrLeftFrontier = screen.offset().left;
            var scrRightFrontier = scrLeftFrontier + screen.outerWidth();
            var scrTopFrontier = screen.offset().top;
            var scrBottomFrontier = scrTopFrontier + screen.outerHeight();
            if (left >= scrLeftFrontier && left <= scrRightFrontier &&
                top >= scrTopFrontier && top <= scrBottomFrontier) {
                screen.addClass("dropable");
                passed = true;
            }
            else {
                screen.removeClass("dropable");
                passed = false;
            }
        }
    };

    /*************** CREATE NEW EDGE **********************************/
    
})
