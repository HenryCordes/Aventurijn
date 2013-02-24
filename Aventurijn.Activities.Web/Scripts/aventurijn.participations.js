
var participations = function (initialdata) {
    var self = this;
    
    self.loading = ko.observableArray();
    self.participations = ko.observableArray(initialdata.Participations);
    self.students = ko.observableArray(initialdata.Students);//ko.mapping.fromJS(initialdata.Students);
    self.activities = ko.observableArray(initialdata.Activities);
   // $("studentForm").validate({ submitHandler: self.save });

    self.load = function() {
        _getData();
    };

    self.save = function() {
        ko.utils.postJson(location.href, { participations: self.participations });
    };
    
    self.addParticipation = function() {
        var now = new Date(); 
        self.participations.push({
            Name: "",
            ParticipationDateTimeAsString: now.toString('dd-mm-yyyy'),
            ParticipationDateTime: now,
            Student: initialdata.Students()[0]
        });
    };
 
    self.removeParticipation = function(participation) {
        self.participations.remove(participation);
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
        self.participations(data);
        self.loading.pop();
    };
}