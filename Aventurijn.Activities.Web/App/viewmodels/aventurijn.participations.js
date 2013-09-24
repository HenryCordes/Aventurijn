
var participations = function(initialdata) {
    var self = this;

    self.isLoading = ko.observable(false);
    self.participations = ko.mapping.fromJS(initialdata.Participations);
    self.students = ko.observableArray(initialdata.Students);
    self.activities = ko.observableArray(initialdata.Activities);
    self.subjects = ko.observableArray(initialdata.Subjects);
    self.modal = {};

    self.save = function() {
        self.isLoading(true);
        var dataAsString = ko.mapping.toJSON(self.participations);
         jQuery.ajax({
                type: 'POST',
                url: 'participation/save',
                contentType: 'application/json',
                dataType: 'json',
                data: dataAsString,
                traditional: true,
                success: function(result){
                    self.isLoading(false);
                    if (result === false) {
                        puntjes.message.show("Opslaan mislukt");
                    }
                    else {
                        _updateModel(result);
                        puntjes.message.show("Succesvol opgeslagen");
                    }
                },
                error: function(xmlHttpRequest)
                {
                    self.isLoading(false);
                    puntjes.message.show(xmlHttpRequest.responseText);
                }
            });
    };
    
    self.addParticipation = function() {
        var now = new Date(); 
        self.participations.push({
            ParticipationId:  ko.observable(0),
            Participating: false,
            ParticipationDateTimeAsString: $.format.date(now,'dd-MM-yyyy'),
            ParticipationDateTime: now,
            StudentId: self.students()[0].StudentId,
            Student: self.students()[0],
            ActivityId: self.activities()[0].ActivityId,
            Activity: self.activities()[0],
            ExtraInfo:''
        });
    };
 
    self.removeParticipation = function(participation) {
        var id = participation.ParticipationId();
        if (id == 0) {
            self.participations.remove(participation);
            puntjes.message.show("Succesvol verwijderd");
        }
        else {

            jQuery.ajax({
                type: 'POST',
                url: 'participation/remove/'+ id,
                traditional: true,
                success: function(result) {
                    if (result == id) {
                        self.participations.remove(participation);
                        puntjes.message.show("Succesvol verwijderd");
                    }
                    else {
                        puntjes.message.show("Verwijderen mislukt");
                    }
                },
                error: function(xmlHttpRequest) {
                    puntjes.message.show("Verwijderen mislukt - fout");
                    $('#exText').val(xmlHttpRequest.responseText);
                }
            });
        }
    };
    
    self.removeParticipationWithId = function(id) {                               
         self.participations.remove(function(participation) { return participation.ParticipationId == id });   
    };

    self.getParticipations = function(from, to, studentId) {
        _getData(from, to, studentId);
    };
    
    self.addActivity =  function(activity) {
        self.activities.push(activity);
        self.activities.sort(function(left, right) { return left.Name == right.Name ? 0 : (left.Name < right.Name ? -1 : 1) });
    };

    self.showNewActivityModal = function() {
         self.modal = $('#new_activity_dialog').modal();
    };

    self.saveNewActivity = function() {
        var activityName = $('#new_activity_name').val();
        var activitySubject = $('#new_activity_subject').val();
        var isActive = $('#new_activity_active').is(":checked");

        var data = {
            'Name': activityName,
            'SubjectId': activitySubject,
            'Active': isActive
        };
        jQuery.ajax({
            type: 'POST',
            url: 'activity/add',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            traditional: true,
            success: function(result) {
                if (result.result === true) {
                    puntjes.message.show("Succesvol opgeslagen");
                    self.addActivity(result.activity);
                    self.modal.close();
                }
                else {
                    puntjes.message.show("Opslaan mislukt");
                }
            },
            error: function(xmlHttpRequest) {
                puntjes.message.show("Opslaan mislukt - fout");
                $('#exText').val(xmlHttpRequest.responseText);
            }
        });
    };

    self.showParticipationsReadOnly = function() {
        var from = $('#fromDate').val();
        var to = $('#toDate').val();
        var studentId = $('#studentsSelect').val();

        window.location.href = 'participation/readonly?from=' + from + '&to=' + to + '&studentId=' + studentId;
    };

    self.setActivityId = function(data, event) {
        var select = $(event.target);
        var newActivityId = select.val();
        var activities = ko.mapping.toJS(self.activities);
        for (var i = 0; i < activities.length; i++) {
            if (activities[i].ActivityId == newActivityId) {
                $(select.parents('tr').find('.subject')[0]).val(activities[i].SubjectId);
                return;
            }
        }
    };

    function _getData (from, to, studentId){
         self.isLoading(true);
        
         var data = { 'from': from, 'to': to, 'studentId': studentId };
        
         jQuery.ajax({
                type: 'POST',
                url: 'participation/withindaterange',
                data: data,
                traditional: true,
                success: function(result){
                    _updateModel(result);
                },
                error: function(xmlHttpRequest)
                {
                    self.isLoading(true);
                    alert(xmlHttpRequest.responseText);
                }
            });
     };
    
    function _updateModel (data) {
        self.isLoading(false);
        ko.mapping.fromJS(data, self.participations);
    };
}