const mongoose  = require('mongoose');

const UserSchema = new mongoose.Schema({
    name:{
        type : String,
        required: true,
    },
    email:{
        type: String,
        required: true,
        unique: true,
        min : 3,
        max: 50
    },
    password:{
        type: String,
        required: true,
    },
    phone:{
        type: String,
        default: "",
    },
    address:{
        type: String,
        default: ""
    }
},{
    timestamps: true
}
)

module.exports = mongoose.model("User",UserSchema);