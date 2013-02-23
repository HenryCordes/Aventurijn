var participations = function () {
    var self = this;
    
    self.loading = ko.observableArray();
    self.participations = ko.observable();

    self.load = function() {
        _getData();
    };

    self.save = function() {
         jQuery.ajax({
                type: 'POST',
                url: 'participation/latestsubscriptions',
                traditional: true,
                success: function(data){
                    _updateModel(data);
                },
                error: function(xmlHttpRequest)
                {
                     self.loading.pop();
                    alert(xmlHttpRequest.responseText);
                }
            });
    };

    function _getData (){
         self.loading.push(true);
        
         jQuery.ajax({
                type: 'GET',
                url: 'participation/all',
                traditional: true,
                success: function(data){
                    _updateModel(data);
                },
                error: function(xmlHttpRequest)
                {
                     self.loading.pop();
                    alert(xmlHttpRequest.responseText);
                }
            });
     };  
    
    function _updateModel (data){
       // var viewModel = ko.mapping.fromJS(data);

        self.participations(data);
        self.loading.pop();
    };
}