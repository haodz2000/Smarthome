const mongoose = require('mongoose')

const GardenSchema = new mongoose.Schema({
    homeId:{
        type: String,
        required: true
    },
    temperature:{
        type: Number,
        default: 30,
    },
    humidity:{
        type: Number,
        default: 75,
    },
    waterPump:{
        type: Boolean,
        default: false,
        // false: Tắt, True: Bật
    }

})

module.exports = mongoose.model('Garden',GardenSchema)