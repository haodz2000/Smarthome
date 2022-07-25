const mongoose = require('mongoose');

const BathRoomSchema = new mongoose.Schema({
    homeId:{
        type: String,
        required: true
    },
    lamp:{
        type: Boolean,
        default: false,
        // false: Tắt, True: Bật
    },
    heater:{
        type: Boolean,
        default: false,
        // false: Tắt, True: Bật
    }
},{timestamps: true})

module.exports = mongoose.model("BathRoom", BathRoomSchema)