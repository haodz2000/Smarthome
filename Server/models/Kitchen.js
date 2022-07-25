const mongoose = require('mongoose')

const KitchenSchema = new mongoose.Schema({
    homeId:{
        type: String,
        required: true
    },
    lamp:{
        type: Boolean,
        default: false,
        // false: Tắt, True: Bật
    },
    waterPump:{
        type: Boolean,
        default: false,
        // false: Tắt, True: Bật
    }
})

module.exports = mongoose.model("Kitchen",KitchenSchema)