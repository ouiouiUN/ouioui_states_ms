db.createUser(
    {
        user: "admin",
        pwd: "12345",
        roles: [
            {
                role: "readWrite",
                db: "state"
            }
        ]
    }
);

db.createCollection("states",{
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: [ "stateId", "userId", "mediaId", "active", "createdOn", "stateText"],
            properties: {
                stateId: { 
                    bsonType: "long" 
                    },
                userId: { 
                    bsonType: "string" 
                    },
                mediaId: { 
                    bsonType: "string"
                    },
                active: {
                    bsonType : "bool"
                },
                createdOn: {
                    bsonType : "date"
                },
                stateText: {
                    bsonType : "string"
                }              
            }
        }
    },
});



