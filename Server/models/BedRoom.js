const mongoose = require('mongoose')

const BedRoomSchema = new mongoose.Schema({
    homeId:{
        type: String,
        required: true
    },
    lamp:{
        type: Boolean,
        default: false,
        // false: Tắt, True: Bật
    },
    airConditioningId:{
        type: String,
        default: "",
        // false: Tắt, True: Bật
    }
},{timestamps:true})

module.exports = mongoose.model("BedRoom",BedRoomSchema)