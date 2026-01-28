
---

#  **AI_NOTES.md**

```markdown
# AI Usage Notes

This document explains how AI tools were used during development of the Weather Tracker coding exercise.

---

## 1. AI Tools Used

- **GitHub Copilot** (inline code suggestions inside VS Code)
- **ChatGPT / Microsoft Copilot** (architecture guidance, code generation, debugging help)

Both tools were used to accelerate development while still maintaining full understanding and control of the final implementation.

---

## 2. Prompts That Helped the Most

### Prompt 1
“Generate a .NET 8 Web API that reads dates from a file, validates them, calls the Open-Meteo Historical Weather API, and stores results as JSON.”

### Prompt 2
“Create a Razor Pages UI that calls a backend API, shows loading/error states, displays a sortable table, and shows details when clicking a row.”

### Prompt 3
“How should I structure services for parsing dates, calling an external API, and caching results in local files?”

These prompts helped shape the architecture and saved time on boilerplate code.

---

## 3. Example Where AI Was Wrong (and How I Fixed It)

One AI suggestion incorrectly used the **Open-Meteo forecast endpoint** instead of the **archive endpoint** required by the assignment:




This endpoint does not support historical data.

I noticed the mismatch because:
- The parameters didn’t match the documentation
- The response structure was different
- The assignment explicitly required `/v1/archive`

I corrected it to:





and adjusted the query parameters accordingly.

---

## 4. Parts Written Manually (Not AI-Generated)

Some parts were intentionally written by hand to ensure clarity and correctness:

- **Date parsing logic**  
  I wanted explicit control over accepted formats and error handling.

- **WeatherService orchestration**  
  This logic determines when to call the API vs. load cached data.  
  I wrote it manually to ensure predictable behavior and clean separation of concerns.

- **Razor Pages sorting and row-selection logic**  
  Copilot suggestions were too generic, so I implemented the sorting and query-string handling myself.

These areas required deliberate reasoning and were better handled manually.

---

## 5. Summary

AI tools were used as accelerators, not replacements.  
They helped with boilerplate, structure, and speed, but all logic was reviewed, corrected, and refined manually to ensure correctness and maintainability.


