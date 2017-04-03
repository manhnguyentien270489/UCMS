angular.module('ngAlert', [])
    .factory('alertSvc', function () {        

        var service = {
            alerts: [],

            add: function(alert) {
                this.alerts.push(alert);                
            },

            addError: function(msg) {
                this.add({
                    id: new Date().getTime(),
                    type: "danger",
                    title: "Error",
                    body: msg
                });
            },

            addInfo: function(msg) {
                this.add({
                    id: new Date().getTime(),
                    type: 'info',
                    title: 'Info',
                    body: msg
                });
            },

            addSuccess: function(msg) {
                this.add({
                    id: new Date().getTime(),
                    type: 'success',
                    title: 'Success',
                    body: msg
                });
            },

            remove: function(alert) {
                for (var i = 0, len = this.alerts.length; i < len; i++) {
                    if (this.alerts[i].id === alert.id) {
                        this.alerts.splice(i, 1);
                        break;
                    }
                }
            },
            clear: function() {
                this.alerts = [];
            }
        }

        return service;
    });