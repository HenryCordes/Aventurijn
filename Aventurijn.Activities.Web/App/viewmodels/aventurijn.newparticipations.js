var newparticipations = function(initialdata) {
    var self = this;

    self.isLoading = ko.observable(false);
    self.participations = ko.mapping.fromJS(initialdata.Participations);
    self.students = ko.mapping.fromJS(initialdata.Students);
    self.levels = ko.observableArray(initialdata.Levels);
    self.activities = ko.observableArray(initialdata.Activities);
    self.subjects = ko.observableArray(initialdata.Subjects);
    self.subjectActivities = ko.observableArray();
    self.levelStudents = ko.observableArray();
    self.subjectId = ko.observable();
    self.activityId = ko.observable();
    self.levelId = ko.observable();
    self.participationDate = ko.observable(initialdata.ParticipationDateAsString);
    self.allSelected = ko.observable(false),
    self.extraInfo = ko.observable(),
    self.modal = {};

   
    self.save = function() {
        self.isLoading(true);

        var studentIds = [];
        ko.utils.arrayForEach(self.levelStudents(), function (student) {
            if (student.IsSelected() === true) {
                studentIds.push(student.StudentId);
            }
        });
        
        if (studentIds.length == 0) {
            self.isLoading(false);
            puntjes.message.show("Kies leerlingen!");
            return;
        }
        var NewParticipationsRequest = {
            StudentIds: studentIds,
            SubjectId: self.subjectId(),
            ActivityId: self.activityId(),
            ExtraInfo: self.extraInfo(),
            ParticipationDate: self.participationDate()
        };
        
        var dataAsString = ko.mapping.toJSON(NewParticipationsRequest);
        jQuery.ajax({
            type: 'POST',
            url: '/participation/new',
            contentType: 'application/json',
            dataType: 'json',
            data: dataAsString,
            traditional: true,
            success: function(result) {
                self.isLoading(false);
                puntjes.message.show("Succesvol toegevoegd");
                _showUpdatedRecords(result);
                ko.utils.arrayForEach(self.levelStudents(), function (student) {
                    student.IsSelected(false);
                });
            },
            error: function(xmlHttpRequest) {
                self.isLoading(false);
                puntjes.message.show("Opslaan mislukt");
            }
        });
    };
    
    function _showUpdatedRecords(result) {
        self.isLoading(false);
        ko.mapping.fromJS(result, self.participations);
    };

    self.setSubject = function (data, event) {
        var select = $(event.target);
        var newSubjectId = select.val();
        self.subjectActivities(_getActivitiesForSubject(newSubjectId));
    };
    
    self.setLevel = function (data, event) {
        var select = $(event.target);
        var newLevelId = select.val();
        self.levelStudents(_getStudentsForLevel(newLevelId));
    };
    
    //self.setActivityId = function (data, event) {
    //    var select = $(event.target);
    //    var newActivityId = select.val();
    //};

    function _getActivitiesForSubject(subjectId) {
        var activitiesToShow = [];
        var allActivities = ko.mapping.toJS(self.activities);
        for (var i = 0; i < allActivities.length; i++) {
            if (allActivities[i].SubjectId == subjectId) {
                activitiesToShow.push(allActivities[i]);
            }
        }
        return activitiesToShow;
    };
    
    function _getStudentsForLevel(levelId) {
        var studentsToShow = [];
        var allStudents = ko.mapping.toJS(self.students);
        for (var i = 0; i < allStudents.length; i++) {
            if (allStudents[i].LevelId == levelId) {
                allStudents[i].IsSelected = ko.observable();
                studentsToShow.push(allStudents[i]);
            }
        }
        return studentsToShow;
    };
    
    self.addActivity = function (activity) {
        self.activities.push(activity);
        self.activities.sort(function (left, right) { return left.Name == right.Name ? 0 : (left.Name < right.Name ? -1 : 1) });
    };
    
    self.showNewActivityModal = function () {
        self.modal = $('#new_activity_dialog').modal();
    };
    
    self.saveNewActivity = function () {
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
            url: '/activity/add',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            traditional: true,
            success: function (result) {
                if (result.result === true) {
                    puntjes.message.show("Succesvol opgeslagen");
                    self.addActivity(result.activity);
                    self.modal.close();
                }
                else {
                    puntjes.message.show("Opslaan mislukt");
                }
            },
            error: function (xmlHttpRequest) {
                puntjes.message.show("Opslaan mislukt - fout");
                $('#exText').val(xmlHttpRequest.responseText);
            }
        });
    };

    self.allSelected.subscribe(function (newValue) {
        ko.utils.arrayForEach(self.levelStudents(), function (student) {
            student.IsSelected(newValue);
        });
    }, self);
};