# HNG12 Stage 3 - Telex Integration

## Project Overview

This project integrates a **tech news monitor** with **Telex**, fetching the most recent tech news from **TechCrunch** and sending it to **Telex** using a webhook.

## Features

- 📰 **Fetch the latest tech news from TechCrunch**
- 🔗 **Send news updates to Telex via webhook**
- ⏳ **Automated fetching via cron job**

## Technologies Used

- **Language:** C#
- **Framework:** .NET 8

## Installation Guide

### Prerequisites
Ensure you have the following installed:

- .NET 8 SDK
- A **Telex** account ([Sign up here](https://telex.im))

### Setup

1. **Clone the repository**
   ```sh
   git clone https://github.com/ikennaumeh/hng12-stage3-telex-integration.git
   cd hng12-stage3-telex-integration
   ```

2. **Set up environment variables**
   Create a `.env` file in the root directory and add the following:
   ```env
   API_KEY=your_newsapi_org_key
   WEBHOOK_URL=your_telex_webhook_url
   ```

3. **Register on Telex & Add the App**
   - Create an account on [Telex.im](https://telex.im).
   - Add the application **tech-news_monitor** to your Telex account.

4. **Start the application**
   ```sh
   dotnet run
   ```

## API Endpoints

- **Trigger News Fetching** `[POST] /api/News/tick`
  - Calls the TechCrunch API, retrieves the latest news, and sends it to Telex.
  - Can be triggered manually or by Telex.

## Usage

No direct usage is required. The Telex system automatically calls the endpoint for you. Alternatively, you can manually trigger it on your local environment using:
```sh
curl -X POST http://localhost:5000/api/News/tick
```
This will send the latest news to your Telex channel.

## Database

- No database is required.

## Authentication

- No authentication is required.

## License

This project is licensed under the **MIT License**.

## Contact

For any inquiries or support, reach out to [@ikennaumeh](https://github.com/ikennaumeh).

---

🚀 **Happy Coding!**

