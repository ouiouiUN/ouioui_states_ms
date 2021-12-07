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
            required: [ "stateId", "userId", "mediaId" ],
            properties: {
                stateId: { 
                    bsonType: "long" 
                    },
                userId: { 
                    bsonType: "string" 
                    },
                mediaId: { 
                    bsonType: "string"
                    }
                
            }
        }
    },
});




db.states.insertOne({
    internalId: {},
    stateId: 0,
    userId: "12345",
    mediaId: "123456"
});