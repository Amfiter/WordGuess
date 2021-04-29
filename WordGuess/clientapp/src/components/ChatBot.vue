<template>
        <div>
                <v-lazy
                        v-model="isActive"
                        :options="{threshold: .2}"
                        min-height="300"
                        transition="fade-transition"
                >
                <v-card class="mx-auto my-12"
                        max-width="400"
                        
                >

                        <Chat
                                style="max-height: 550px; min-height: 550px;"
                                :scroll-bottom="scrollBottom"
                                class="overflow-y-auto"
                                :participants="participants"
                                :myself="myself"
                                :messages="messages"
                                :placeholder="placeholder"
                                :colors="colors"
                                :border-style="borderStyle"
                                :load-more-messages="toLoad.length > 0 ? loadMoreMessages : null"
                                :async-mode="asyncMode"
                                :profile-picture-config="profilePictureConfig"
                                :timestamp-config="timestampConfig"
                                @onMessageSubmit="onMessageSubmit"
                                @onType="onType"
                                @messageGet="created"
                                >
                                
                        </Chat>
                </v-card>
                </v-lazy>
        </div>
       
</template>

<script>
        import {Chat} from 'vue-quick-chat';
        import 'vue-quick-chat/dist/vue-quick-chat.css';

        export default {
                components: {
                        Chat
                },
                data() {
                        return {
                                rm:[],
                                isActive: false,
                                visible: true,
                                participants: [
                                        {
                                                name: 'Система',
                                                id: 1,
                                                profilePicture: 'https://www.promarinetrade.com/cache/promarine/public/shop_product_picture/_1200x800x0/4630_S.jpg'
                                        },
                                ],
                                myself: {
                                        name: 'Вы',
                                        id: 2,
                                        profilePicture: 'https://www.promarinetrade.com/cache/promarine/public/shop_product_picture/_1200x800x0/4624_M.jpg'
                                },
                                messages: [
                                        /*{
                                                content: 'вавава',
                                                myself: false,
                                                participantId: 1,
                                                timestamp: {year: 2019, month: 3, day: 5, hour: 20, minute: 10, second: 3, millisecond: 123},
                                                type: 'text'
                                        },
                                        {
                                                content: 'sent messages',
                                                myself: true,
                                                participantId: 2,
                                                timestamp: {year: 2019, month: 4, day: 5, hour: 19, minute: 10, second: 3, millisecond: 123},
                                                type: 'text'
                                        },*/
                                ],
                                placeholder: 'Напишите вашу догадку',
                                colors: {
                                        message: {
                                                myself: {
                                                        bg: '#fff',
                                                        text: '#746e6e'
                                                },
                                                others: {
                                                        bg: '#0989f3',
                                                        text: '#fff'
                                                },
                                                messagesDisplay: {
                                                        bg: '#f7f3f3'
                                                }
                                        },
                                        submitIcon: '#093e6c',
                                       
                                },
                                borderStyle: {
                                        topLeft: "10px",
                                        topRight: "10px",
                                        bottomLeft: "10px",
                                        bottomRight: "10px",
                                },
                                asyncMode: false,
                                toLoad: [
                                        {
                                                content: 'Хэй, привет! Я загадал слово, твоя задача его отгадать, ПОЕХАЛИ!',
                                                myself: false,
                                                participantId: 1,
                                                timestamp: {year: 2011, month: 3, day: 5, hour: 10, minute: 10, second: 3, millisecond: 123},
                                                uploaded: true,
                                                viewed: true,
                                                type: 'text'
                                        },
                                        
                                        
                                ],
                                scrollBottom: {
                                        messageSent: true,
                                        messageReceived: false,
                                },
                                profilePictureConfig: {
                                        others: true,
                                        myself: true,
                                        styles: {
                                                width: '30px',
                                                height: '30px',
                                                borderRadius: '50%'
                                        }
                                },
                                timestampConfig: {
                                        format: 'HH:mm',
                                        relative: false
                                },
                                
                        }
                },
                methods: {
                        
                        
                        onType: function () {
                                //here you can set any behavior
                        },
                        loadMoreMessages(resolve) {
                                setTimeout(() => {
                                        resolve(this.toLoad); //We end the loading state and add the messages
                                        //Make sure the loaded messages are also added to our local messages copy or they will be lost
                                        this.messages.unshift(...this.toLoad);
                                        this.toLoad = [];
                                        try {
                                                this.axios.get("http://localhost:5000/api/secretword").then(response => {
                                                        this.rm = response.data;
                                                        this.messages.push(response.data);
                                                });
                                        } catch (e) {
                                                console.error(e);
                                        }
                                }, 1000);
                        },
                        async created() {
                                
                        },

                        async onMessageSubmit(message) {
                                /*
                                * example simulating an upload callback. 
                                * It's important to notice that even when your message wasn't send 
                                * yet to the server you have to add the message into the array
                                */
                                this.messages.push(message);
                                const payload = {
                                        
                                        content: message.content,
                                };
                                try {
                                        await this.axios.post('http://localhost:5000/api/message', payload)
                                                .then(response => {
                                                        if (response) {
                                                                this.message = [...response.data];
                                                        } else {
                                                                // eslint-disable-next-line no-console
                                                                console.log('response null')
                                                        }
                                                });
                                } catch (error) {
                                        confirm(error)
                                }
                                try {
                                        this.axios.get("http://localhost:5000/api/message").then(response => {
                                                this.rm = response.data;
                                                this.messages.push(response.data);
                                        });
                                } catch (e) {
                                        console.error(e);
                                }
                                /*
                                * you can update message state after the server response
                                */
                                // timeout simulating the request
                                setTimeout(() => {
                                        message.uploaded = true
                                }, 2000)
                        },
                }
        }
</script>

<style scoped>
</style>