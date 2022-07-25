const mongoose = require('mongoose');

const LivingRoomSchema = new mongoose.Schema({
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
        type:String,
        default: ""
    }
},{timestamps: true}
)

module.exports = mongoose.model("LivingRoom", LivingRoomSchema);