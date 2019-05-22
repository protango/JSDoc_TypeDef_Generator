/**
 * Enter Description Here
 * @typedef {Object} Teacher_1
 * @property {Faculty[]} Faculties 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {any[]} Unavailables 
 * @property {number} ActualLoad 
 * @property {string} Code 
 * @property {boolean} ConsecutivePeriods 
 * @property {number} DaysUnavailable 
 * @property {boolean} Eligible 
 * @property {number} FinalLoad 
 * @property {number} MaxYDLoad 
 * @property {number} Order 
 * @property {boolean} PeriodsOff 
 * @property {number} PeriodsUnavailable 
 * @property {number} PersonID 
 * @property {number} Priority 
 * @property {number} ProposedAllotment 
 * @property {number} ProposedLoad 
 * @property {string} SpareField1 
 * @property {string} SpareField2 
 * @property {string} SpareField3 
 * @property {number} TeacherID 
 * @property {number} Uncounted 
 * @property {number} CreatedBy 
 * @property {Date} CreatedOn 
 * @property {number} EditedBy 
 * @property {Date} EditedOn 
 * @property {string} Email 
 * @property {string} FirstName 
 * @property {string} FullName 
 * @property {string} LastName 
 * @property {string} MiddleName 
 * @property {string} Mobile 
 * @property {string} Phone 
 * @property {boolean} Status 
 * @property {string} Title 
 * @property {string} Display 
 * @property {number[]} FacultyIDs 
 * @property {Course[]} Courses 
 * @property {any[]} Meetings 
 * @property {string} $$hashKey 
 * @property {UnavailableDisplay[]} UnavailableDisplay 
 * @property {CourseDisplay[]} CourseDisplay 
 */
/**
 * Enter Description Here
 * @typedef {Object} CourseDisplay
 * @property {Course} Course 
 * @property {UnavailableDisplay[]} Days 
 * @property {string} $$hashKey 
 */
/**
 * Enter Description Here
 * @typedef {Object} Period_2
 * @property {Period} Period 
 * @property {string} Designation 
 * @property {string} $$hashKey 
 */
/**
 * Enter Description Here
 * @typedef {Object} UnavailableDisplay
 * @property {Day} Day 
 * @property {(Period_1|Period_2)[]} Periods 
 * @property {string} $$hashKey 
 */
/**
 * Enter Description Here
 * @typedef {Object} Period_1
 * @property {Period} Period 
 * @property {boolean} HasAllocation 
 * @property {string} Unavailable 
 * @property {string} Description 
 * @property {string} $$hashKey 
 */
/**
 * Enter Description Here
 * @typedef {Object} Day
 * @property {string} Code 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {string} Name 
 * @property {number} Number 
 * @property {Period[]} Periods 
 * @property {string} $$hashKey 
 */
/**
 * Enter Description Here
 * @typedef {Object} Period
 * @property {string} Code 
 * @property {string} DayCode 
 * @property {number} DayID 
 * @property {number} DayNumber 
 * @property {boolean} Doubles 
 * @property {string} EndTime 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {number} Index 
 * @property {number} Load 
 * @property {string} Name 
 * @property {number} Number 
 * @property {boolean} Quadruples 
 * @property {boolean} SiteMove 
 * @property {string} StartTime 
 * @property {boolean} Triples 
 * @property {Day} Day 
 * @property {string} $$hashKey 
 */
/**
 * Enter Description Here
 * @typedef {Object} Clash
 * @property {number} PeriodID 
 * @property {any} Source 
 * @property {Course} Target 
 * @property {any} Teacher 
 * @property {any} Room 
 * @property {string} Reason 
 * @property {number} Type 
 */
/**
 * Enter Description Here
 * @typedef {Object} RollClassGroup
 * @property {Instance[]} Allocations 
 * @property {string} Block 
 * @property {number} ColourCode 
 * @property {number} CreatedBy 
 * @property {Date} CreatedOn 
 * @property {number} Doubles 
 * @property {number} EditedBy 
 * @property {Date} EditedOn 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {number} Load 
 * @property {number} MRCGId 
 * @property {number} Periods 
 * @property {number} Quadruples 
 * @property {number} RollClassID 
 * @property {number} Triples 
 * @property {any[]} Unavailables 
 * @property {string} LoadDisplay 
 * @property {MRCG} MRCG 
 * @property {RollClass} RollClass 
 * @property {Course[]} Courses 
 */
/**
 * Enter Description Here
 * @typedef {Object} Course
 * @property {number} ClassID 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {Instance[]} Instances 
 * @property {number} RollClassGroupID 
 * @property {Room[]} Rooms 
 * @property {number} Row 
 * @property {boolean} ShareRooms 
 * @property {boolean} ShareTeachers 
 * @property {Teacher[]} Teachers 
 * @property {number[]} RoomIDs 
 * @property {number[]} TeacherIDs 
 * @property {Class} Class 
 * @property {Subject} Subject 
 * @property {number[]} FacultyIDs 
 * @property {Faculty_1} Faculty 
 * @property {Room_1} Room 
 * @property {Teacher_1} Teacher 
 * @property {RollClassGroup} RollClassGroup 
 * @property {MRCG} Group 
 * @property {Clash[]} Clashes 
 * @property {Course[]} SharedTeachers 
 * @property {Course[]} SharedRooms 
 */
/**
 * Enter Description Here
 * @typedef {Object} Room_1
 * @property {number} FileID 
 * @property {number} RoomID 
 * @property {any[]} Unavailables 
 * @property {number} Capacity 
 * @property {string} Code 
 * @property {number} CreatedBy 
 * @property {Date} CreatedOn 
 * @property {number} EditedBy 
 * @property {Date} EditedOn 
 * @property {number} ID 
 * @property {string} Name 
 * @property {string} Notes 
 * @property {number} Number 
 * @property {string} RoomKey 
 * @property {number} SchoolID 
 * @property {number} Site 
 * @property {string} Display 
 * @property {Course[]} Courses 
 */
/**
 * Enter Description Here
 * @typedef {Object} RollClass
 * @property {string} Code 
 * @property {number} CreatedBy 
 * @property {Date} CreatedOn 
 * @property {number} EditedBy 
 * @property {Date} EditedOn 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {string} Name 
 * @property {number} YearLevelID 
 * @property {any} Groups 
 * @property {any} YearLevel 
 */
/**
 * Enter Description Here
 * @typedef {Object} MRCG
 * @property {string} Code 
 * @property {number} CreatedBy 
 * @property {Date} CreatedOn 
 * @property {string} DefaultCode 
 * @property {number} EditedBy 
 * @property {Date} EditedOn 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {string} Name 
 * @property {any} RollClassGroupIDs 
 * @property {any} Groups 
 */
/**
 * Enter Description Here
 * @typedef {Object} Faculty_1
 * @property {number} FacultyID 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {string} Code 
 * @property {number} CreatedBy 
 * @property {Date} CreatedOn 
 * @property {number} EditedBy 
 * @property {Date} EditedOn 
 * @property {string} Name 
 * @property {number} SchoolID 
 */
/**
 * Enter Description Here
 * @typedef {Object} Class
 * @property {string} BOSCode1 
 * @property {string} BOSCode2 
 * @property {string} BOSCode3 
 * @property {string} Code 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {any[]} Lines 
 * @property {string} Name 
 * @property {Student[]} Students 
 * @property {number} SubjectID 
 * @property {string} Display 
 * @property {Subject} Subject 
 * @property {number[]} FacultyIDs 
 */
/**
 * Enter Description Here
 * @typedef {Object} Subject
 * @property {number} AutoInstances 
 * @property {number} Balance1 
 * @property {number} Balance2 
 * @property {number} CodeGenScheme 
 * @property {boolean} Core 
 * @property {Faculty[]} Faculties 
 * @property {number} FileID 
 * @property {number} ID 
 * @property {number} Instances 
 * @property {boolean} IsUnscheduled 
 * @property {number} SubjectID 
 * @property {boolean} AllowGenderBreaches 
 * @property {boolean} AllowPrerequisiteBreaches 
 * @property {string} Code 
 * @property {boolean} CorrespondingLines 
 * @property {number} CreatedBy 
 * @property {Date} CreatedOn 
 * @property {string} Description 
 * @property {number} EditedBy 
 * @property {Date} EditedOn 
 * @property {number} Load 
 * @property {number} MaxClassSize 
 * @property {string} Name 
 * @property {number} OrderNo 
 * @property {number} SchoolID 
 * @property {boolean} Status 
 * @property {number} Subgrid 
 * @property {string} SubjectKey 
 * @property {number} YearLevel 
 * @property {string} Display 
 */
/**
 * Enter Description Here
 * @typedef {Object} Student
 * @property {string} LessonPriority 
 * @property {string} LessonType 
 * @property {number} LinkID 
 * @property {number} StudentID 
 */
/**
 * Enter Description Here
 * @typedef {Object} Teacher
 * @property {number} LinkID 
 * @property {number} TeacherID 
 */
/**
 * Enter Description Here
 * @typedef {Object} Room
 * @property {string} LessonPriority 
 * @property {number} LinkID 
 * @property {number} RoomID 
 */
/**
 * Enter Description Here
 * @typedef {Object} Instance
 * @property {boolean} Blocking 
 * @property {number} LinkID 
 * @property {boolean} Manual 
 * @property {number} PeriodID 
 */
/**
 * Enter Description Here
 * @typedef {Object} Faculty
 * @property {number} FacultyID 
 * @property {number} LinkID 
 * @property {number} MaximumLoad 
 * @property {number} MinimumLoad 
 */
